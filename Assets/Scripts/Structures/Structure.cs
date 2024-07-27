using UnityEngine;

public class Structure : PoolableMonobehaviour, ISelectableEntity
{
    [SerializeField] private float radius = 5.0f;
    [SerializeField] private GameObject highlightFloor;
    [SerializeField] private EntityHighlight entityHighLight;

    private void OnValidate()
    {
        entityHighLight = GetComponent<EntityHighlight>();
    }

    public Vector3 RandomPositionAroundStructure()
    {
        Vector3 randomPosition = Random.insideUnitCircle.normalized;
        randomPosition.z = randomPosition.y;
        randomPosition *= radius;
        Vector3 finalPosition = transform.position;
        finalPosition.y = 0.0f;
        finalPosition += randomPosition;

        return finalPosition;
    }

    public virtual void SelectEntity()
    {
        highlightFloor.SetActive(true);
    }

    public virtual void UnSelectEntity()
    {
        highlightFloor.SetActive(false);
    }

    protected override void OnEnable()
    {

    }

    public void ShowAsObjective()
    {
        entityHighLight.TapHighlight();
    }
}
