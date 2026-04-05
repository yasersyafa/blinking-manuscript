using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgotPassword : DelayedDialogueState, IBathroomHandler, ISinkHandler
{
    protected override string DialogueObjectName => "ForgotPassword";

    protected override float Delay => 1f;

    public void OnBathroomClicked(GameStateManager manager)
    {
        MainSceneManager.ins.bgBathroom.SetActive(true);
    }

    public void OnSinkClicked(GameStateManager manager)
    {
        manager.SetState(manager.bathroom1);
    }
}
