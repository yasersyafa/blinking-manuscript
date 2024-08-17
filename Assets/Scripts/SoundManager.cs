using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    public AudioClip normal, mindblink, ending;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(!audioSource.isPlaying)
        {
            PlayMusic();
        }
    }

    public void PlayMusic()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        AudioClip newClip = null;
        if(sceneName == "Main Scene" || sceneName == "Outside" || sceneName == "Cutscene Intro" || sceneName == "Transition Day")
        {
            newClip = normal;
        }
        else if(sceneName == "Mindblink1" || sceneName == "Mindblink2" || sceneName == "Mindblink3")
        {
            newClip = mindblink;
        }
        else if(sceneName == "Cutscene Ending")
        {
            newClip = ending;
        }

        if (audioSource.clip != newClip)
        {
            StartCoroutine(FadeOutAndIn(newClip));
        }
    }

    IEnumerator FadeOutAndIn(AudioClip newClip)
    {
        yield return StartCoroutine(FadeOut(1f));
        audioSource.clip = newClip;
        audioSource.Play();
        yield return StartCoroutine(FadeIn(1f));
    }

    IEnumerator FadeOut(float duration)
    {
        float startVolume = audioSource.volume;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }
        audioSource.volume = 0;
    }

    private IEnumerator FadeIn(float duration)
    {
        audioSource.volume = 0;
        float targetVolume = 1f;  // Volume target setelah fade in selesai

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, targetVolume, t / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private void OnLevelWasLoaded(int index)
    {
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
