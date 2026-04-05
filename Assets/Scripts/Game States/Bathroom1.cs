using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathroom1 : DelayedDialogueState
{
    protected override string DialogueObjectName => "Bathroom1";
    protected override float Delay => 0.5f;
    protected override IState NextState(GameStateManager m) => m.mindblink1;
}
