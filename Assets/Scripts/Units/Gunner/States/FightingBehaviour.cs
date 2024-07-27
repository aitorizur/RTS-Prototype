[System.Serializable]
public class FightingBehaviour : State<Unit>
{
    public StateMachine<Gunner> gunnerStatemachine;

    public Shooting shooting;
    public GoingToEnemy goingToEnemy;

    public override void OnEnter()
    {
        gunnerStatemachine.currentState.OnEnter();
    }

    public override void Run()
    {
        gunnerStatemachine.Run();
    }

    public override void OnExit()
    {

    }

    public override void InitializeState(StateMachine<Unit> givenStateMachine)
    {
        base.InitializeState(givenStateMachine);
        gunnerStatemachine = new StateMachine<Gunner>(new State<Gunner>[] { shooting, goingToEnemy }, (Gunner)stateMachine.parent);
        gunnerStatemachine.SwapToState(typeof(GoingToEnemy));
    }
}
