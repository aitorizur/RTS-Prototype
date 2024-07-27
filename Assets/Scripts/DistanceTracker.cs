using System.Collections.Generic;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    [SerializeField] private int pairsToUpdatePerFrame = 3;

    private List<Unit> redUnits = new List<Unit>();
    private List<Unit> blueUnits = new List<Unit>();

    private int currentRedIndex = 0;
    private int currentBlueIndex = 0;

    public void AddUnit(Unit unitToAdd)
    {
        if (unitToAdd.Team == ArmyManager.Team.Red)
        {
            redUnits.Add(unitToAdd);
        }
        else
        {
            blueUnits.Add(unitToAdd);
        }
    }

    private void Update()
    {
        if (redUnits.Count > 0 && blueUnits.Count > 0)
        {
            for (int i = 0; i < pairsToUpdatePerFrame; i++)
            {
                float distance = Vector3.Distance(redUnits[currentRedIndex].transform.position, 
                                                  blueUnits[currentBlueIndex].transform.position);
                if (distance <= redUnits[currentRedIndex].VisionRange && redUnits[currentRedIndex] is Gunner)
                {
                    Gunner redGunner = (Gunner)redUnits[currentRedIndex];
                    redGunner.GoToIfIddle(blueUnits[currentBlueIndex]);
                }

                AddToIndexes();
            }
        }
    }

    private void AddToIndexes()
    {
        ++currentRedIndex;
        if (currentRedIndex >= redUnits.Count)
        {
            currentRedIndex = 0;
            ++currentBlueIndex;
            if (currentBlueIndex >= blueUnits.Count)
            {
                currentBlueIndex = 0;
                currentRedIndex = 0;
            }
        }
    }

    public void RemoveUnit(Unit unitToRemove)
    {
        if (unitToRemove.Team == ArmyManager.Team.Red)
        {
            redUnits.Remove(unitToRemove);
            if (currentRedIndex > 0)
            {
                --currentRedIndex;
            }
        }
        else
        { 
            blueUnits.Remove(unitToRemove);
            if (currentBlueIndex > 0)
            {
                --currentBlueIndex;
            }
        }
    }
}
