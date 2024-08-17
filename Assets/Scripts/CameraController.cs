using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 minBounds; // Batas minimum (kiri bawah)
    public Vector2 maxBounds;

    private Camera cam;
    private float camHalfHeight;
    private float camHalfWidth;

    private Vector2 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        cam = Camera.main;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = cam.aspect * camHalfHeight;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // Menghitung posisi baru kamera
        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);

        // Membatasi posisi kamera agar tidak melewati batas background
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);

        transform.position = newPosition;
    }
}
