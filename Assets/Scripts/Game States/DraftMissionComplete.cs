using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftMissionComplete : IState
{
    private Dialogue dialogue;
    public bool hasDone = false;
    public void OnEnter(GameStateManager manager)
    {
        dialogue = GameObject.Find("DraftComplete").GetComponent<Dialogue>();
        dialogue.TriggerDialogue();
    }

    public void OnExecute(GameStateManager manager)
    {
        if(hasDone)
        {
            manager.SetState(manager.sendDraft);
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }
}
