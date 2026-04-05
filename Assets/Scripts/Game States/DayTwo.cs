public class DayTwo : SceneLoadState, ITypeWriterHandler
{
    protected override string TargetScene => "Transition Day";
    protected override float DelayAfterLoad => 1.2f;

    public void OnTypeWriterFinished(GameStateManager manager)
    {
        manager.SetState(new LoadMainSceneForDay2());
    }

    protected override void OnSceneReady(GameStateManager manager)
    {
        // manager.SetState(manager.computerScene2);
    }
}

public class LoadMainSceneForDay2 : SceneLoadState
{
    protected override string TargetScene => "Main Scene";
    protected override float DelayAfterLoad => 1.2f;

    protected override void OnSceneReady(GameStateManager manager)
    {
        manager.SetState(manager.computerScene2);
    }
}
