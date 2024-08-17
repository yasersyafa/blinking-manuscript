using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mindblink3 : IState
{
    private float countdown = 1.3f;
    Dialogue dialogue;
    bool isDone;
    public bool isWin;
    public void OnEnter(GameStateManager manager)
    {
        SceneController.Ins.LoadScene("Mindblink3");
        countdown = 1.3f;
        isDone = false;
        isWin = false;
    }

    public void OnExecute(GameStateManager manager)
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0 && !isDone)
        {
            isDone = true;
            dialogue = GameObject.Find("Mindblink3").GetComponent<Dialogue>();
            dialogue.TriggerDialogue();
        }

        if(isWin && DialogueManager.ins.isDone)
        {
            manager.SetState(manager.endingCompleted);
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }
}
