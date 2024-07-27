using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PositionSelectionVisual : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        gameObject.SetActive(false);
        animator = GetComponent<Animator>();
    }

    public void SetToPosition(Vector3 newPosition)
    { 
        gameObject.SetActive(true);
        animator.Play("Default", -1 ,0.0f);
        transform.position = newPosition;
    }

    public void DisableGameobject()
    {
        gameObject.SetActive(false);
    }
}
