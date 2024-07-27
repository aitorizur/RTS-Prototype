using UnityEngine;

[System.Serializable]
public class GoingToEnemy : GoingTo<Gunner>
{
    public override void Run()
    {
        if (IsAtObjective(ClosestPointOnMesh(stateMachine.parent.targetEnemy.transform.position), stateMachine.parent.gunMaxRange))
        {
            stateMachine.SwapToState(typeof(Shooting));
        }
        else
        { 
            SetDestination(ClosestPointOnMesh(stateMachine.parent.targetEnemy.transform.position));
        }
    }
}
