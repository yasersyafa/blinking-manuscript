using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter(GameStateManager manager);
    void OnExecute(GameStateManager manager);
    void OnExit(GameStateManager manager);
}
