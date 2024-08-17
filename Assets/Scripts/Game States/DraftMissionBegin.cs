using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftMissionBegin : IState
{
    private Dialogue dialogue;
    public bool isFileManager = false;
    public void OnEnter(GameStateManager manager)
    {
        dialogue = GameObject.Find("DraftBegin").GetComponent<Dialogue>();
        dialogue.TriggerDialogue();
    }

    public void OnExecute(GameStateManager manager)
    {
        if(isFileManager)
        {
            manager.SetState(manager.draftMissionForgot);
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }
}
