using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private Transform startingTransform;
    [SerializeField] private GameObject mineralPrefab;

    public int maxCapacity;
    public int CurrentCarrying = 0;

    private GameObject[] mineralVisuals;

    public bool IsFull { get { return CurrentCarrying == maxCapacity; } }

    private void Awake()
    {
        SetInstanceVariables();
        SpawnMineralVisuals();
        EmptyBackpack();
    }

    private void SetInstanceVariables()
    { 
        mineralVisuals = new GameObject[maxCapacity];
    }

    private void SpawnMineralVisuals()
    {
        float mineralVisualHeight = mineralPrefab.transform.localScale.y;
        mineralVisuals = new GameObject[maxCapacity];

        for (int i = 0; i < maxCapacity; i++)
        {
            Vector3 offset = transform.up * (i * mineralVisualHeight * 1.1f);
            mineralVisuals[i] = Instantiate(mineralPrefab, startingTransform.position + offset, startingTransform.rotation, startingTransform);
        }
    }

    public void AddToBackpack(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (CurrentCarrying == maxCapacity)
            {
                return;
            }
            mineralVisuals[CurrentCarrying].SetActive(true);
            CurrentCarrying += 1;
        }
    }

    public void EmptyBackpack()
    {
        CurrentCarrying = 0;
        foreach (GameObject currentMineralVisual in mineralVisuals)
        {
            currentMineralVisual.SetActive(false);
        }
    }
}
