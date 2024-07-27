using UnityEngine;

public class Mineral : Structure
{
    [SerializeField] private GameObject mineralHUD;

    public override void SelectEntity()
    {
        base.SelectEntity();
        mineralHUD.SetActive(true);
    }

    public override void UnSelectEntity()
    {
        base.UnSelectEntity();
        mineralHUD.SetActive(false);
    }
}
