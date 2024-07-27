using UnityEngine;

public class BaseFunctionality : MonoBehaviour
{
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private MonobehaviourPool<Unit>[] unitPool;

    private ArmyManager armyManager;
    private Base selectedBuilding;

    private void OnValidate()
    {
        armyManager = FindObjectOfType<ArmyManager>(true);
    }

    public void SetBuilding(Base building)
    {
        selectedBuilding = building;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (resourceManager.ModifyResources(-5))
            {
                SpawnUnit(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (resourceManager.ModifyResources(-10))
            {
                SpawnUnit(1);
            }
        }
    }

    public void AddResources(int valueToAddOrSubstract)
    {
        resourceManager.ModifyResources(valueToAddOrSubstract);
    }

    public void SpawnUnit(int index)
    {
        Unit clone = unitPool[index].InstantiateMonobehaviour(selectedBuilding.RandomPositionAroundStructure());
        armyManager.AddUnit(clone);
        clone.armyManager = armyManager;
    }
}
