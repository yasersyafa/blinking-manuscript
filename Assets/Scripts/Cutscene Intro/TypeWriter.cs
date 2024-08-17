using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TypeWriter : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    [TextArea(3, 10)]public string fullText = "Humans often grapple with fleeting memories and unexpected bursts of inspiration. Sometimes, ideas come to us in the most peculiar places—whether it’s in a mundane moment or an ordinary setting. This strange phenomenon, where creativity strikes out of the blue, is known as... Mindblink.";
    private float delay = 0.05f;
    private AudioSource audioSource;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        textMesh = GetComponent<TextMeshProUGUI>();
        if(GameStateManager.Ins != null && GameStateManager.Ins.currentState == GameStateManager.Ins.day3) fullText = "The next day...";
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        textMesh.text = "";
        yield return new WaitForSeconds(1f);
        

        foreach(char c in fullText)
        {
            AudioClip newClip = GetRandomClips();
            textMesh.text += c;
            audioSource.PlayOneShot(newClip);
            yield return new WaitForSeconds(delay);
        }
        if(GameStateManager.Ins != null)
        {
            if(GameStateManager.Ins.currentState == GameStateManager.Ins.day2)
            {
                GameStateManager.Ins.day2.isMainScene = true;
            }
            else if(GameStateManager.Ins.currentState == GameStateManager.Ins.day3)
            {
                GameStateManager.Ins.day3.isMainScene = true;
            }
        }
        yield return new WaitForSeconds(1f);
        // pindah scene
        SceneController.Ins.LoadScene("Main Scene");
    }

    public AudioClip GetRandomClips()
    {
        int index = Random.Range(0, clips.Length);
        AudioClip newClip = clips[index];
        return newClip;
    }
}
