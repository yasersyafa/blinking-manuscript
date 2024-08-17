using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day3 : IState
{
    public bool isMainScene = false;
    private float countdown = 1.2f;
    public void OnEnter(GameStateManager manager)
    {
        SceneController.Ins.LoadScene("Transition Day");
    }

    public void OnExecute(GameStateManager manager)
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0)
        {
            if(SceneManager.GetActiveScene().name == "Main Scene" && isMainScene)
            {
                // on active scene
                
                // set next state
                manager.SetState(manager.outside1);
            }
        }
    }

    public void OnExit(GameStateManager manager)
    {
        
    }
}
