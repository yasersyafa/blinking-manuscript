using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetImage : MonoBehaviour
{
    public static SetImage instance;
    private Animator animator;
    [TextArea(3, 10)] public string endingText;
    public bool isFirst = false;

    void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public void SetNormalAnim()
    {
        isFirst = true;
    }

    public void StartAnimation()
    {
        animator.Play("endingImage");
    }

    public void StartType()
    {
        StartCoroutine(TypeEnding());
    }

    public void Animator2()
    {
        animator.Play("prologueActive");
    }

    private IEnumerator TypeEnding()
    {
        TextMeshProUGUI textAnim = GetComponentInChildren<TextMeshProUGUI>();
        textAnim.text = "";
        foreach(char c in endingText)
        {
            textAnim.text += c;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
