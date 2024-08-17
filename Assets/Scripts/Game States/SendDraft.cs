using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendDraft : IState
{
    private bool isActive = false;
    private bool isDone = false;
    public bool isCorrectDraft = false;
    public void OnEnter(GameStateManager manager)
    {
        SceneController.Ins.LoadScene("Main Scene");
    }

    public void OnExecute(GameStateManager manager)
    {
        if(SceneManager.GetActiveScene().name == "Main Scene" && !isActive)
        {
            OnActiveScene();
            isActive = true;
        }
        if(isCorrectDraft && !isDone)
        {
            Dialogue dialogue = GameObject.Find("DraftSendComplete").GetComponent<Dialogue>();
            isDone = true;
            dialogue.TriggerDialogue();
        }
        if(isDone && DialogueManager.ins.isDone)
        {
            // next state cutscene
            manager.SetState(manager.day2);
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }

    private void OnActiveScene()
    {
        MainSceneManager.ins.canvasFocused.SetActive(false);
        MainSceneManager.ins.canvasTable.SetActive(false);
        MainSceneManager.ins.lockScreen.SetActive(false);
        MainSceneManager.ins.screenLocked.SetActive(false);

        MainSceneManager.ins.mxWriter.SetActive(false);
        MainSceneManager.ins.MxWriter.SetActive(false);
        MainSceneManager.ins.FileManager.SetActive(true);
        MainSceneManager.ins.fileManager.SetActive(true);
    }
}
