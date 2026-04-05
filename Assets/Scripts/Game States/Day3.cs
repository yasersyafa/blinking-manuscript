public class Day3 : SceneLoadState, ITypeWriterHandler
{
    protected override string TargetScene => "Transition Day";
    protected override float DelayAfterLoad => 1.2f;

    public void OnTypeWriterFinished(GameStateManager manager)
    {
        manager.SetState(new LoadMainSceneForDay3());
    }

    protected override void OnSceneReady(GameStateManager manager)
    {
        
    }
}

public class LoadMainSceneForDay3 : SceneLoadState
{
    protected override string TargetScene => "Main Scene";
    protected override float DelayAfterLoad => 1.2f;

    protected override void OnSceneReady(GameStateManager manager)
    {
        manager.SetState(manager.outside1);
    }
}