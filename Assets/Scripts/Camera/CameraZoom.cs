using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomingSpeed;
    [SerializeField] private float zoomSmoothnes = 10.0f;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    private const string ZoomAxis = "Mouse ScrollWheel";

    private float targetHeight;

    private void OnValidate()
    {
        SetInstanceVariables();
    }

    private void SetInstanceVariables()
    { 
        targetHeight = transform.position.y;
    }

    void Update()
    {
        UpdateTargetZoom();
        ZoomCamera();
    }

    private void UpdateTargetZoom()
    {
        float zoomAmount = -Input.GetAxisRaw(ZoomAxis) * zoomingSpeed;
        targetHeight += zoomAmount;
        targetHeight = Mathf.Clamp( targetHeight, minHeight, maxHeight);
    }

    private void ZoomCamera()
    {
        Vector3 tempPosition = transform.position;
        tempPosition.y = Mathf.Lerp(transform.position.y, targetHeight, Time.deltaTime * zoomSmoothnes);
        transform.position = tempPosition;
    }
}
