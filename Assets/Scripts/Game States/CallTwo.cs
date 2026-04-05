public class CallTwo : BaseCallState
{
    protected override string DialogueObjectName => "CallTwo";

    protected override IState NextState(GameStateManager manager)
    {
        return manager.publisherThree;
    }
}
