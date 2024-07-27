using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class GoingTo<T> : State<T> where T : Unit
{
    public float DistanceToConsiderReach = 2.0f;

    protected Vector3 destination;

    public override void OnEnter()
    {
        SetDestination(destination);
    }

    public override void OnExit()
    {
        stateMachine.parent.NavAgent.ResetPath();
    }

    public override void Run()
    {
        if (IsAtObjective(stateMachine.parent.NavAgent.destination, DistanceToConsiderReach))
        {
            stateMachine.SwapToState(typeof(Idle<T>));
        }
    }

    public override void InitializeState(StateMachine<T> givenStateMachine)
    {
        base.InitializeState(givenStateMachine);
        stateMachine.parent.NavAgent.ResetPath();
    }

    public bool IsAtObjective(Vector3 objective, float minDistance)
    {
        return Vector3.Distance(objective, stateMachine.parent.NavAgent.transform.position) <= minDistance;
    }

    public void SetDestination(Vector3 position)
    {
        destination = position;
        stateMachine.parent.NavAgent.destination = position;
    }

    public Vector3 ClosestPointOnMesh(Vector3 position)
    {
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(position, out closestHit, 10.0f, NavMesh.AllAreas))
        {
            return closestHit.position;
        }
        else
        {
            return Vector3.zero;
        }

    }
}
