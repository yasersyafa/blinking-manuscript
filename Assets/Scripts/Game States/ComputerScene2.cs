using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScene2 : DelayedDialogueState
{
    protected override string DialogueObjectName => "ComputerScene2";
    protected override float Delay => 1f;

    public override void OnEnter(GameStateManager manager)
    {
        SetupUI();
        base.OnEnter(manager);
    }

    public override void OnExecute(GameStateManager manager)
    {
        base.OnExecute(manager); // handle dialogue trigger

        // behavior tambahan — transisi via canvasTable
        if (MainSceneManager.ins.canvasTable.activeSelf)
            manager.SetState(manager.callTwo);
    }

    private void SetupUI()
    {
        MainSceneManager.ins.canvasFocused.SetActive(false);
        MainSceneManager.ins.canvasTable.SetActive(false);
        MainSceneManager.ins.mxWriter.SetActive(false);
        MainSceneManager.ins.MxWriter.SetActive(false);
        MainSceneManager.ins.fileManager.SetActive(false);
        MainSceneManager.ins.FileManager.SetActive(false);
        MainSceneManager.ins.shutdownScreen.SetActive(true);
        MainSceneManager.ins.screenShutdown.SetActive(true);
    }
}
