using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Mining : State<Miner>
{
    [SerializeField] private float timeToMine = 1.0f;

    private float initialTimeToMine;

    public float NormalizedMinigProgress { get { return timeToMine / initialTimeToMine; } }

    public override void OnExit()
    {

    }

    public override void Run()
    {
        if (stateMachine.parent.miningBeheaviour.goingToMine.IsAtObjective(stateMachine.parent.targetMineralPosition, stateMachine.parent.miningBeheaviour.goingToMine.DistanceToConsiderReach))
        {
            Mine();
        }
        else
        {
            stateMachine.SwapToState(typeof(GoingToMine));
        }
        
    }

    private void Mine()
    {
        timeToMine += Time.deltaTime;
        if (timeToMine >= initialTimeToMine)
        {
            timeToMine = 0.0f;
            stateMachine.parent.backpack.AddToBackpack(1);
            if (stateMachine.parent.backpack.IsFull)
            {
                stateMachine.SwapToState(typeof(GoingToBase));
            }
        }
    }

    public override void OnEnter()
    {
        timeToMine = 0.0f;
    }

    public override void InitializeState(StateMachine<Miner> givenStateMachine)
    {
        base.InitializeState(givenStateMachine);
        initialTimeToMine = timeToMine;
    }
}
