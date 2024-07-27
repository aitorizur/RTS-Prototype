using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float panSpeed;

    private Camera cam;

    private void Awake()
    {
        SetInstanceVariables();
    }

    private void SetInstanceVariables()
    { 
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 mousePosition = cam.ScreenToViewportPoint(Input.mousePosition);

        mousePosition.x = (int)Mathf.Clamp(mousePosition.x * 2 - 1, -1.0f, 1.0f);
        mousePosition.z = (int)Mathf.Clamp(mousePosition.y * 2 - 1, -1.0f, 1.0f);
        mousePosition.y = 0.0f;

        transform.position += mousePosition.normalized * panSpeed * Time.deltaTime;
    }
}
