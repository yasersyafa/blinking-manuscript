using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublisherOne : IState
{
    private Dialogue dialogue;
    private float countdown;
    private bool isDone;
    public void OnEnter(GameStateManager manager)
    {
        countdown = 1f;
        isDone = false;
        dialogue = GameObject.Find("PublisherOne").GetComponent<Dialogue>();
    }

    public void OnExecute(GameStateManager manager)
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0 && !isDone)
        {
            isDone = true;
            dialogue.TriggerDialogue();
        }
        if(isDone && DialogueManager.ins.isDone)
        {
            manager.SetState(manager.inputNamePlayer);
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }
}
