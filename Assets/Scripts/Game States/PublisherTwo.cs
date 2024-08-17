using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublisherTwo : IState
{
    private Dialogue dialogue1, dialogue2;
    private float countdown;
    private bool isDone;
    public void OnEnter(GameStateManager manager)
    {
        dialogue1 = GameObject.Find("PublisherTwo").GetComponent<Dialogue>();
        dialogue1.TriggerDialogue();
        dialogue2 = GameObject.Find("You1").GetComponent<Dialogue>();
        countdown = 1f;
        isDone = false;
    }

    public void OnExecute(GameStateManager manager)
    {
        
        if(DialogueManager.ins.isDone)
        {
            countdown -= Time.deltaTime;
            if(countdown <= 0 && !isDone)
            {
                isDone = true;
                dialogue2.TriggerDialogue();
            }
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }
}
