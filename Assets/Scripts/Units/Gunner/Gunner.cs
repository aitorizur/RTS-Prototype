using UnityEngine;

public class Gunner : Unit
{
    public FightingBehaviour fightingBehaviour = new FightingBehaviour();
    public float gunMaxRange = 10.0f;

    [HideInInspector] public Unit targetEnemy = null;

    protected override void InitializeStateMachine()
    {
        stateMachine = new StateMachine<Unit>(new State<Unit>[] { idle, goingTo, fightingBehaviour }, this);
    }

    public override void GoTo(Unit unit)
    {
        if (unit.Team != Team)
        {
            targetEnemy = unit;
            stateMachine.SwapToState(typeof(FightingBehaviour));
        }
    }

    public void GoToIfIddle(Unit unit)
    {
        if (stateMachine.currentState is Idle<Unit>)
        {
            GoTo(unit);
        }
    }
}
