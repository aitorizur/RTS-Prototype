using UnityEngine;

public class Base : Structure
{
    [SerializeField] private BaseFunctionality baseFunctionality;

    private void OnValidate()
    {
        baseFunctionality = FindObjectOfType<BaseFunctionality>(true);
    }

    public void AddResources(int value)
    {
        baseFunctionality.AddResources(value);
    }

    public override void SelectEntity()
    {
        base.SelectEntity();
        baseFunctionality.gameObject.SetActive(true);
        baseFunctionality.SetBuilding(this);
    }

    public override void UnSelectEntity()
    {
        base.UnSelectEntity();
        baseFunctionality.gameObject.SetActive(false);
    }
}
