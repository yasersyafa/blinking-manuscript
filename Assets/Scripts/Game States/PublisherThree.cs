using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublisherThree : IState
{
    private float countdown = 0.5f;
    private bool isDone = false;
    Dialogue dialogue;
    public void OnEnter(GameStateManager manager)
    {
        dialogue = GameObject.Find("PublisherThree").GetComponent<Dialogue>();
        Debug.Log("publisher 3 on action");
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
            manager.SetState(manager.endingBegin);
        }
        
    }

    public void OnExit(GameStateManager manager)
    {
        
    }

    void NextStaet()
    {

    }
}
