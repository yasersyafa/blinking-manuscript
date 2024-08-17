using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetPassword : IState
{
    private bool isDone;
    private float countdown;
    private bool dialogueDone;
    Dialogue dialogue;
    public void OnEnter(GameStateManager manager)
    {
        SceneController.Ins.LoadScene("Main Scene");
        isDone = false;
        countdown = 1f;
        
        
        dialogueDone = false;
    }

    public void OnExecute(GameStateManager manager)
    {
        if(SceneManager.GetActiveScene().name == "Main Scene" && !isDone)
        {
            isDone = true;
            ActivateBathroom();
        }

        if(isDone)
        {
            countdown -= Time.deltaTime;
            if(!dialogueDone && countdown <= 0)
            {
                dialogue = GameObject.Find("GetPassword").GetComponent<Dialogue>();
                dialogueDone = true;
                dialogue.TriggerDialogue();
            }
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }

    void ActivateBathroom()
    {
        MainSceneManager.ins.bgBathroom.SetActive(true);
    }
}
