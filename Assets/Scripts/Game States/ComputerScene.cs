using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScene : IState
{
    private Dialogue dialogue;
    private float duration;
    private bool isDone;

    private GameStateManager stateManager = GameStateManager.Ins;
    public void OnEnter(GameStateManager manager)
    {
        MainSceneManager.ins.lockScreen.SetActive(false);
        MainSceneManager.ins.screenLocked.SetActive(false);
        dialogue = GameObject.Find("ComputerScene").GetComponent<Dialogue>();
        duration = 1.3f;
        isDone = false;
    }

    public void OnExecute(GameStateManager manager)
    {
        duration -= Time.deltaTime;
        if(duration <= 0 && !isDone)
        {
            isDone = true;
            dialogue.TriggerDialogue();
        }

    }

    public void OnExit(GameStateManager manager)
    {
        
    }
}
