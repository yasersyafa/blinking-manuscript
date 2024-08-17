using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgotPassword : IState
{
    private float countdown;
    public bool isDone;
    private Dialogue dialogue;
    
    public void OnEnter(GameStateManager manager)
    {
        dialogue = GameObject.Find("ForgotPassword").GetComponent<Dialogue>();
        countdown = 0.5f;
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
