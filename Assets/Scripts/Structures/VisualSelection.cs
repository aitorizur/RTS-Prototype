using UnityEngine;

public class VisualSelection : MonoBehaviour
{
    [SerializeField] private RectTransform visualPanelRect;

    private Vector2 initialPosition;

    private void Awake()
    {
            visualPanelRect.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            visualPanelRect.gameObject.SetActive(true);
            initialPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            float height = Input.mousePosition.y - initialPosition.y;
            float width = Input.mousePosition.x - initialPosition.x;
            visualPanelRect.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
            visualPanelRect.anchoredPosition = initialPosition + new Vector2(width, height) * 0.5f;

        }

        if (Input.GetMouseButtonUp(0))
        {
            visualPanelRect.gameObject.SetActive(false);
            initialPosition = Input.mousePosition;
        }
    }
}
