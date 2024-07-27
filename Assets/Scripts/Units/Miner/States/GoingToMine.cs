using UnityEngine;

[System.Serializable]
public class GoingToMine : GoingTo<Miner>
{
    public override void OnEnter()
    {
        if (stateMachine.parent.targetMineral == null)
        {
            FindClosestMineral();
        }

        SetDestination(stateMachine.parent.targetMineralPosition);
    }

    private void FindClosestMineral()
    {
        float distance = 100000.0f;
        foreach (Mineral currentMineral in Object.FindObjectsOfType<Mineral>())
        {
            float newDistance = Vector3.Distance(stateMachine.parent.transform.position, currentMineral.transform.position);
            if (newDistance < distance)
            {
                distance = newDistance;
                SetMineral(currentMineral);
            }
        }
    }

    public override void Run()
    {
        if (IsAtObjective(stateMachine.parent.NavAgent.destination, DistanceToConsiderReach))
        {
            stateMachine.SwapToState(typeof(Mining));
        }
    }

    public void SetMineral(Mineral givenMineral)
    {
        stateMachine.parent.targetMineral = givenMineral;
        stateMachine.parent.targetMineralPosition = ClosestPointOnMesh(stateMachine.parent.targetMineral.RandomPositionAroundStructure());
    }
}
