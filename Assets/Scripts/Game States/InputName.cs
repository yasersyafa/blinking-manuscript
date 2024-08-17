using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputName : IState
{
    public bool isFilled;
    public void OnEnter(GameStateManager manager)
    {
        MainSceneManager.ins.bgInputField.SetActive(true);
        isFilled = false;
    }

    public void OnExecute(GameStateManager manager)
    {
        if(isFilled)
        {
            manager.SetState(manager.publisherTwo);
        }
    }

    public void OnExit(GameStateManager manager)
    {
        MainSceneManager.ins.bgInputField.SetActive(false);
    }
}
