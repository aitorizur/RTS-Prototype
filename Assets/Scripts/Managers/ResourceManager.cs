using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resourceText = null;

    [SerializeField] private int resources = 100;

    private void Update()
    {
        UpdateResourceText();    
    }

    private void UpdateResourceText()
    {
        resourceText.text = resources.ToString();
    }

    public bool ModifyResources(int valueToAddOrSubstract)
    {
        if (resources + valueToAddOrSubstract >= 0)
        {
            resources += valueToAddOrSubstract;
            return true;
        }
        else
        {
            return false;
        }
    }
}
