public abstract class State<T>
{
    protected StateMachine<T> stateMachine;

    public abstract void Run();
    public abstract void OnExit();
    public abstract void OnEnter();
    public virtual void InitializeState(StateMachine<T> givenStateMachine)
    {
        stateMachine = givenStateMachine;
    }
}
