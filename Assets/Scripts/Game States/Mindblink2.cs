using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mindblink2 : IState
{
    public bool isComplete = false;
    public bool isDone;
    private Dialogue dialogue;
    public void OnEnter(GameStateManager manager)
    {
        SceneController.Ins.LoadScene("Mindblink2");
        
        isDone = false;
    }

    public void OnExecute(GameStateManager manager)
    {
        if(!isDone && SceneManager.GetActiveScene().name == "Mindblink2")
        {
            isDone = true;
            dialogue = GameObject.Find("Mindblink2").GetComponent<Dialogue>();
            dialogue.TriggerDialogue();
        }
        if(isComplete)
        {
            manager.SetState(manager.draftMissionComplete);
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }
}
