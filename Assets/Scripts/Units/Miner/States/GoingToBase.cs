using UnityEngine;

[System.Serializable]
public class GoingToBase : GoingTo<Miner>
{
    public override void OnEnter()
    {
        if (stateMachine.parent.targetBase == null)
        {
            FindClosestBase();
        }

        SetDestination(ClosestPointOnMesh(stateMachine.parent.targetBase.RandomPositionAroundStructure()));
    }

    private void FindClosestBase()
    {
        float distance = 100000.0f;
        foreach (Base currentBase in GameObject.FindObjectsOfType<Base>())
        {
            float newDistance = Vector3.Distance(stateMachine.parent.transform.position, currentBase.transform.position);
            if (newDistance < distance)
            {
                distance = newDistance;
                stateMachine.parent.targetBase = currentBase;
            }
        }
    }

    public override void Run()
    {
        if (IsAtObjective(stateMachine.parent.NavAgent.destination, DistanceToConsiderReach))
        {
            stateMachine.parent.backpack.EmptyBackpack();
            stateMachine.parent.targetBase.AddResources(stateMachine.parent.backpack.maxCapacity);
            stateMachine.SwapToState(typeof(GoingToMine));
        }
    }

    public void SetBase(Base givenBase)
    {
        stateMachine.parent.targetBase = givenBase;
    }
}
