using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEndingBegin : DelayedDialogueState, IShutdownHandler
{
    protected override string DialogueObjectName => "You2";

    protected override float Delay => 1f;

    public void OnShutdownClicked(GameStateManager manager)
    {
        manager.SetState(manager.mindblink3);
    }
}
