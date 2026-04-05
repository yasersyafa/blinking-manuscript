using UnityEngine;

public class Mindblink2 : SceneLoadState
{
    protected override string TargetScene => "Mindblink2";
    protected override float DelayAfterLoad => 0f; // langsung setelah scene ready

    public bool isComplete = false;

    public override void OnEnter(GameStateManager manager)
    {
        isComplete = false;
        base.OnEnter(manager);
    }

    protected override void OnSceneReady(GameStateManager manager)
    {
        var obj = GameObject.Find("Mindblink2");
        if (obj == null)
        {
            Debug.LogError("[Mindblink2] 'Mindblink2' tidak ditemukan!");
            return;
        }
        obj.GetComponent<Dialogue>()?.TriggerDialogue();
    }

    public override void OnExecute(GameStateManager manager)
    {
        base.OnExecute(manager);

        if (isComplete)
            manager.SetState(manager.draftMissionComplete);
    }

    public override void OnExit(GameStateManager manager)
    {
        base.OnExit(manager);
        isComplete = false;
    }
}