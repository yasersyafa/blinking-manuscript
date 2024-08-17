using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScene2 : IState
{
    private Dialogue dialogue;
    private float countdown = 1f;
    private bool isDone = false;
    public void OnEnter(GameStateManager manager)
    {
        OnActiveScene();
        dialogue = GameObject.Find("ComputerScene2").GetComponent<Dialogue>();
        
    }

    public void OnExecute(GameStateManager manager)
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0 && !isDone)
        {
            isDone = true;
            dialogue.TriggerDialogue();
        }
        if(MainSceneManager.ins.canvasTable.activeSelf)       
        {
            // next state callTwo
            manager.SetState(manager.callTwo);
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }

    void OnActiveScene()
    {
        MainSceneManager.ins.canvasFocused.SetActive(false);
        MainSceneManager.ins.canvasTable.SetActive(false);

        // disable all apps
        MainSceneManager.ins.mxWriter.SetActive(false);
        MainSceneManager.ins.MxWriter.SetActive(false);
        MainSceneManager.ins.fileManager.SetActive(false);
        MainSceneManager.ins.FileManager.SetActive(false);

        MainSceneManager.ins.shutdownScreen.SetActive(true);
        MainSceneManager.ins.screenShutdown.SetActive(true);
    }
}
