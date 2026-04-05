using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublisherThree : DelayedDialogueState
{
    protected override string DialogueObjectName => "PublisherThree";

    protected override float Delay => 1f;
    protected override IState NextState(GameStateManager manager)
    {
        return manager.endingBegin;
    }
}
