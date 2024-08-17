using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathroom1 : IState
{
    private float countdown;
    private bool isDone;
    private Dialogue dialogue;
    public void OnEnter(GameStateManager manager)
    {
        dialogue = GameObject.Find("Bathroom1").GetComponent<Dialogue>();
        isDone = false;
        countdown = 0.5f;
    }

    public void OnExecute(GameStateManager manager)
    {
        countdown -= Time.deltaTime;
        if(!isDone && countdown <= 0)
        {
            isDone = true;
            dialogue.TriggerDialogue();
        }
        if(DialogueManager.ins.isDone && isDone)
        {
            manager.SetState(manager.mindblink1);
        }
        
    }

    public void OnExit(GameStateManager manager)
    {
        
    }
}
