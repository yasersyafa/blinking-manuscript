using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DelayedDialogueState : IState
{
    private float _countdown;
    private bool _triggered;
    private Dialogue _dialogue;

    // Wajib di-implement child class
    protected abstract string DialogueObjectName { get; }
    protected abstract float Delay { get; }

    // Opsional — return null kalau state ini tidak auto-transition
    protected virtual IState NextState(GameStateManager manager) => null;

    public virtual void OnEnter(GameStateManager manager)
    {
        _countdown = Delay;
        _triggered = false;

        var obj = GameObject.Find(DialogueObjectName);
        if (obj == null)
        {
            Debug.LogError($"[DelayedDialogueState] '{DialogueObjectName}' tidak ditemukan!");
            return;
        }
        _dialogue = obj.GetComponent<Dialogue>();
    }

    public virtual void OnExecute(GameStateManager manager)
    {
        _countdown -= Time.deltaTime;

        if (_countdown <= 0 && !_triggered)
        {
            _triggered = true;
            _dialogue?.TriggerDialogue();
        }

        if (_triggered && DialogueManager.ins.isDone)
        {
            var next = NextState(manager);
            if (next != null)
                manager.SetState(next);
        }
    }

    public virtual void OnExit(GameStateManager manager) { }
}

public class PublisherOne : DelayedDialogueState
{
    protected override string DialogueObjectName => "PublisherOne";

    protected override float Delay => 1f;
    protected override IState NextState(GameStateManager manager)
    {
        return manager.inputNamePlayer;
    }
}
