using UnityEngine;

[System.Serializable]
public class Shooting : State<Gunner>
{
    [SerializeField] private float fireRate = 1.0f;
    [SerializeField] private float damage = 10.0f;
    [SerializeField] ParticleSystem wazzleParticleEffect;

    private float initialFireRate;

    public override void OnEnter()
    {
        fireRate = initialFireRate;
        SwapToGoingToEnemyIfNotAtRange();
    }

    public override void OnExit()
    {

    }

    public override void Run()
    {
        fireRate -= Time.deltaTime;

        if (fireRate <= 0.0f)
        {
            float enemyHealth = Shoot(stateMachine.parent.targetEnemy);

            if (enemyHealth <= 0.0f)
            {
                stateMachine.parent.GoIdle();
            }
            else
            {
                SwapToGoingToEnemyIfNotAtRange();
            }
        }
    }

    private void SwapToGoingToEnemyIfNotAtRange()
    {
        if (!stateMachine.parent.fightingBehaviour.goingToEnemy.IsAtObjective(
             stateMachine.parent.fightingBehaviour.goingToEnemy.ClosestPointOnMesh(stateMachine.parent.targetEnemy.transform.position), stateMachine.parent.gunMaxRange))
        {
            stateMachine.SwapToState(typeof(GoingToEnemy));
        }
    }

    private float Shoot(Unit objective)
    {
        fireRate = initialFireRate;
        wazzleParticleEffect.Clear();
        wazzleParticleEffect.gameObject.SetActive(true);
        wazzleParticleEffect.Play();
        return objective.TakeDamage(damage);
    }

    public override void InitializeState(StateMachine<Gunner> givenStateMachine)
    {
        base.InitializeState(givenStateMachine);
        initialFireRate = fireRate;
    }
}
