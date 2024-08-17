using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonEffect : MonoBehaviour
{
    private TextMeshProUGUI buttonText;
    private Color normalColor;
    // Start is called before the first frame update
    void Start()
    {
        buttonText = GetComponent<TextMeshProUGUI>();
        normalColor = buttonText.color;
    }

    public void OnButtonEnter()
    {
        buttonText.color = Color.white;
    }

    public void OnButtonExit()
    {
        buttonText.color = normalColor;
    }
}
