using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Ins;
    private Animator animator;

    void Awake()
    {
        Ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(FadeTransition(scene));
    }

    private IEnumerator FadeTransition(string nameScene)
    {
        animator.SetTrigger("load");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nameScene);
    }
}
