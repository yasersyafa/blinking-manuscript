using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mindblink3 : DelayedDialogueState
{
    protected override string DialogueObjectName => "Mindblink3";
    protected override float Delay => 1.3f;
    public bool isWin;

    public override void OnEnter(GameStateManager manager)
    {
        SceneController.Ins.LoadScene("Mindblink3");
        isWin = false;
        base.OnEnter(manager);
    }

    public override void OnExecute(GameStateManager manager)
    {
        base.OnExecute(manager);

        if (isWin && DialogueManager.ins.isDone)
            manager.SetState(manager.endingCompleted);
    }
}
