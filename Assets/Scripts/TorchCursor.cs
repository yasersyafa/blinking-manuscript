using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchCursor : MonoBehaviour
{
    public Camera mainCamera; // Referensi ke kamera utama
    private Vector3 mousePosition;
    public static bool isMoving = true;

    void Update()
    {
        if(isMoving)
        {
            CursorGlow();
        }
    }

    void CursorGlow()
    {
        mousePosition = Input.mousePosition;
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        // ikutin arah cursor
        transform.position = mousePosition;
    }
}
