using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    private TextMeshProUGUI titleText;
    private string text = "Blinking Manuscript: Prologue";
    private float delay = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        titleText = GetComponent<TextMeshProUGUI>();
        
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        while(true)
        {
            titleText.text = "";
            foreach(char letter in text)
            {
                titleText.text += letter;
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(1f);
            for(int i = text.Length; i >= 0; i--)
            {
                titleText.text = titleText.text.Substring(0, i);
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
