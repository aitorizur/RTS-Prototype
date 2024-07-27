using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class EntityHighlight : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    [SerializeField] private float defaultTapTime = 0.5f;
    [SerializeField] private Color baseHighlightColor = Color.red;
    private Color initialColor;

    private void OnValidate()
    {
        rend = GetComponent<Renderer>();
    }

    private void Awake()
    {
        initialColor = rend.material.color;
    }

    public void Highlight()
    {
        rend.material.color = baseHighlightColor;
    }

    public void Highlight(Color highlightColor)
    {
        rend.material.color = highlightColor;
    }

    public void StopHighlight()
    {
        rend.material.color = initialColor;
    }

    public void TapHighlight()
    {
        TapHighLightForTime(defaultTapTime);
    }

    public void TapHighLightForTime(float tapTime)
    {
        StopAllCoroutines();
        StartCoroutine(CoroutineTapHighlightForTime(tapTime));
    }

    private IEnumerator CoroutineTapHighlightForTime(float tapTime)
    {
        float timer = 1.0f;
        while (timer >= 0.0f)
        {
            rend.material.color = Color.Lerp(initialColor, baseHighlightColor, timer);
            timer -= Time.deltaTime / tapTime;
            yield return null;
        }
    }
}
