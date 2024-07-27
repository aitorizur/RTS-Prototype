[System.Serializable]
public class MiningBehaviour : State<Unit>
{
    public StateMachine<Miner> miningStatemachine;

    public GoingToMine goingToMine;
    public Mining mining;
    public GoingToBase goingToBase;

    public override void OnEnter()
    {
        miningStatemachine.currentState.OnEnter();
    }

    public override void OnExit()
    {

    }

    public override void Run()
    {
        miningStatemachine.Run();
    }

    public override void InitializeState(StateMachine<Unit> givenStateMachine)
    {
        base.InitializeState(givenStateMachine);
        miningStatemachine = new StateMachine<Miner>(new State<Miner>[] {goingToMine, goingToBase, mining }, (Miner)stateMachine.parent);
        miningStatemachine.SwapToState(typeof(GoingToMine));
    }
}
