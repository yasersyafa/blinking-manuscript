using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEndingBegin : IState
{
    Dialogue dialogue;
    private float countdown = 0.5f;
    private bool isDone;
    public void OnEnter(GameStateManager manager)
    {
        dialogue = GameObject.Find("You2").GetComponent<Dialogue>();
        isDone = false;
    }

    public void OnExecute(GameStateManager manager)
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0 && !isDone)
        {
            isDone = true;
            dialogue.TriggerDialogue();
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }
}
