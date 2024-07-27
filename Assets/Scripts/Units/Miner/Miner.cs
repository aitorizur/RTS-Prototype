using UnityEngine;

public class Miner : Unit
{
    public MiningBehaviour miningBeheaviour = new MiningBehaviour();
    public Backpack backpack = null;

    [HideInInspector] public Base targetBase = null;
    [HideInInspector] public Mineral targetMineral = null;
    [HideInInspector] public Vector3 targetMineralPosition = default;

    protected override void InitializeStateMachine()
    {
        targetMineral = null;
        stateMachine = new StateMachine<Unit>(new State<Unit>[] { idle, goingTo, miningBeheaviour }, this);
        stateMachine.SwapToState(typeof(Idle<Unit>));
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        backpack.EmptyBackpack();
    }

    public override void GoTo(Structure targetStructure)
    {
        if (targetStructure is Base)
        {
            miningBeheaviour.goingToBase.SetBase((Base)targetStructure);
        }
        else if (targetStructure is Mineral)
        {
            miningBeheaviour.goingToMine.SetMineral((Mineral)targetStructure);
        }

        stateMachine.SwapToStateOrRenter(typeof(MiningBehaviour));
    }
}
