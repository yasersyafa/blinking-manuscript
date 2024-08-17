using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class SoundEffect : MonoBehaviour
{
    public AudioClip click, phone, glitch;
    [HideInInspector] public AudioSource audioSource;
    public static SoundEffect Ins;

    void Awake()
    {
        if(Ins == null)
        {
            Ins = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(click);
        }
    }
}
