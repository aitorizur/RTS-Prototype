using UnityEngine;
using UnityEngine.AI;

public class Unit : PoolableMonobehaviour, ISelectableEntity
{
    public ArmyManager armyManager;
    public EntityHighlight EntityHighlight;
    public NavMeshAgent NavAgent;
    public ArmyManager.Team Team;
    public float VisionRange = 10.0f;

    [Header("Unit Parameters")]

    [SerializeField] private float health = 100.0f;

    [Header("States")]

    [SerializeField] protected Idle<Unit> idle = new Idle<Unit>();
    [SerializeField] protected GoingTo<Unit> goingTo = new GoingTo<Unit>();

    [SerializeField] protected StateMachine<Unit> stateMachine;


    private void OnValidate()
    {
        SetInstanceVariables();
    }

    private void SetInstanceVariables()
    {
        EntityHighlight = GetComponent<EntityHighlight>();
        NavAgent = GetComponent<NavMeshAgent>();
        armyManager = FindObjectOfType<ArmyManager>();
    }

    private void Awake()
    {
        InitializeStateMachine();
    }

    protected virtual void InitializeStateMachine()
    { 
        stateMachine = new StateMachine<Unit>(new State<Unit>[] { idle, goingTo}, this);
        stateMachine.SwapToState(typeof(Idle<Unit>));
    }

    protected virtual void Update()
    {
        stateMachine.Run();
    }

    public void GoTo(Vector3 position)
    {
        goingTo.SetDestination(position);
        stateMachine.SwapToState(typeof(GoingTo<Unit>));
    }

    public virtual void GoTo(Structure targetStructure)
    {
        GoTo(targetStructure.RandomPositionAroundStructure());
    }

    public virtual void GoTo(Unit unit)
    { 
        
    }

    public virtual void GoIdle()
    {
        stateMachine.SwapToState(typeof(Idle<Unit>));
    }

    protected override void OnEnable()
    {
        GoTo(transform.position);
        if (!armyManager.ContainsUnit(this))
        {
            armyManager.AddUnit(this);
        }
    }

    private void Start()
    {
        GoTo(transform.position);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EntityHighlight.StopHighlight();
        armyManager.RemoveUnit(this);
    }

    public void SelectEntity()
    {
        EntityHighlight.Highlight();
    }

    public void UnSelectEntity()
    {
        EntityHighlight.StopHighlight();
    }

    public void ShowAsObjective()
    {
        EntityHighlight.TapHighlight();
    }

    public float TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0.0f)
        {
            gameObject.SetActive(false);
        }

        return health;
    }
}
