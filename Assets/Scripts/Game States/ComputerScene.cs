using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScene : DelayedDialogueState, IShutdownHandler
{
    protected override string DialogueObjectName => "ComputerScene";
    protected override float Delay => 1.3f;
    
    public override void OnEnter(GameStateManager manager)
    {
        MainSceneManager.ins.lockScreen.SetActive(false);
        MainSceneManager.ins.screenLocked.SetActive(false);
        base.OnEnter(manager);
    }

    public void OnShutdownClicked(GameStateManager manager)
    {
        MainSceneManager.ins.lockScreen.SetActive(true);
        MainSceneManager.ins.shutdownScreen.SetActive(true);
        MainSceneManager.ins.screenLocked.SetActive(true);
        MainSceneManager.ins.screenShutdown.SetActive(true);
        manager.SetState(manager.callOne);
    }
}
