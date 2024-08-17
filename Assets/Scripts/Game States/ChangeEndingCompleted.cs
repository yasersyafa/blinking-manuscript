using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeEndingCompleted : IState
{
    private bool isActiveScene;
    private float countdown;
    private bool isDone;
    public void OnEnter(GameStateManager manager)
    {
        SceneController.Ins.LoadScene("Main Scene");
        isActiveScene = false;
        countdown = 1f;
        isDone = false;
    }

    public void OnExecute(GameStateManager manager)
    {
        if(SceneManager.GetActiveScene().name == "Main Scene" && !isActiveScene)
        {
            OnActive();
            isActiveScene = true;
        }
        if(isActiveScene)
        {
            countdown -= Time.deltaTime;
            if(countdown <= 0 && !isDone)
            {
                isDone = true;
                Dialogue dialogue = GameObject.Find("You3").GetComponent<Dialogue>();
                dialogue.TriggerDialogue();
            }
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }

    void OnActive()
    {
        MainSceneManager.ins.canvasTable.SetActive(true);
        MainSceneManager.ins.shutdownScreen.SetActive(false);
        MainSceneManager.ins.screenShutdown.SetActive(false);
        MainSceneManager.ins.screenLocked.SetActive(false);
        MainSceneManager.ins.lockScreen.SetActive(false);


        // active writer app
        MainSceneManager.ins.mxWriter.SetActive(true);
        MainSceneManager.ins.MxWriter.SetActive(true);
        MainSceneManager.ins.fileManager.SetActive(false);
        MainSceneManager.ins.FileManager.SetActive(false);
    }
}
