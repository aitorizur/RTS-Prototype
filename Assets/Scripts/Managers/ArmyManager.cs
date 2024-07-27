using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ArmyManager : MonoBehaviour
{
    [SerializeField] private EntitySelector entitySelector = new EntitySelector();
    [SerializeField] private DistanceTracker distanceTracker = null;
    [SerializeField] private RectTransform selectionRect = null;
    [SerializeField] private PositionSelectionVisual positionSelection = null;
    [SerializeField] private Team teamToSelect = Team.Red;
    [SerializeField] private LayerMask selectionLayer;
    [SerializeField] private LayerMask groundLayer;

    public enum Team { Red, Blue}
    [SerializeField] private List<Unit> units = new List<Unit>();
    private List<Unit> selectedUnits = new List<Unit>();
    private Camera cam;
    private bool isSelecting = false;

    private void Awake()
    {
        SetInstanceVariables();
    }

    private void SetInstanceVariables()
    {
        cam = GetComponent<Camera>();
        distanceTracker = FindObjectOfType<DistanceTracker>();
    }

    private void Update()
    {
        CheckSelection();
        CheckMoveOrder();
    }

    private void CheckMoveOrder()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hitInfo;
            bool hasHit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo, 10000.0f, selectionLayer);
            if (hasHit)
            {
                ISelectableEntity targetedEntity;
                if (hitInfo.collider.gameObject.TryGetComponent<ISelectableEntity>(out targetedEntity))
                {
                    if (targetedEntity is Structure)
                    {
                        foreach (Unit currentUnit in selectedUnits)
                        {
                            currentUnit.GoTo((Structure)targetedEntity);
                        }
                    }
                    else if (targetedEntity is Unit)
                    {
                        foreach (Unit currentUnit in selectedUnits)
                        {
                            currentUnit.GoTo((Unit)targetedEntity);
                        }
                    }

                    targetedEntity.ShowAsObjective();
                }
            }
            else
            {
                MoveSelectedUnitsToMousePosition();
            }
        }
    }

    private void MoveSelectedUnitsToMousePosition()
    {
        RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo, 10000.0f, groundLayer);
        if (hasHit && selectedUnits.Count > 0)
        {
            foreach (Unit currentUnit in selectedUnits)
            {
                currentUnit.GoTo(hitInfo.point);
            }

            ShowTargetPositionGroundVisual(hitInfo.point);
        }
    }

    private void ShowTargetPositionGroundVisual(Vector3 targetPosition)
    {
        targetPosition.y += 0.2f;
        positionSelection.SetToPosition(targetPosition);
    }

    private void CheckSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClearSelection();

            RaycastHit hitInfo;
            bool hasHit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo, 10000.0f, selectionLayer);
            if (hasHit)
            {
                ISelectableEntity targetedEntity;
                if (hitInfo.collider.gameObject.TryGetComponent<ISelectableEntity>(out targetedEntity))
                {
                    entitySelector.UnselectEntities();
                    entitySelector.AddEntity(targetedEntity);
                    
                    if (targetedEntity is Unit)
                    {
                        Unit targetedUnit = (Unit)targetedEntity;

                        if (targetedUnit.Team == teamToSelect)
                        {
                            selectedUnits.Add(targetedUnit);
                        }
                    }
                }
            }
            else
            {
                isSelecting = true;
                entitySelector.UnselectEntities();
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (isSelecting)
            {
                ClearSelection();

                Vector2 min = selectionRect.anchoredPosition - (selectionRect.sizeDelta * 0.5f);
                Vector2 max = selectionRect.anchoredPosition + (selectionRect.sizeDelta * 0.5f);

                foreach (Unit currentUnit in units)
                {
                    if (currentUnit.Team == teamToSelect)
                    {
                        Vector2 unitPositionOnScreen = cam.WorldToScreenPoint(currentUnit.transform.position);

                        if (unitPositionOnScreen.x > min.x && unitPositionOnScreen.x < max.x &&
                            unitPositionOnScreen.y > min.y && unitPositionOnScreen.y < max.y)
                        {
                            selectedUnits.Add(currentUnit);
                            entitySelector.AddEntity(currentUnit);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSelecting = false;
        }
    }

    private void ClearSelection()
    {
        foreach (Unit currentUnit in selectedUnits)
        {
            currentUnit.UnSelectEntity();
        }
        selectedUnits.Clear();
    }

    public void AddUnit(Unit unitToAdd)
    {
        units.Add(unitToAdd);
        distanceTracker.AddUnit(unitToAdd);
    }

    public void RemoveUnit(Unit unitToRemove)
    {
        units.Remove(unitToRemove);
        selectedUnits.Remove(unitToRemove);
        distanceTracker.RemoveUnit(unitToRemove);
    }

    public bool ContainsUnit(Unit unitToCheck)
    {
        return units.Contains(unitToCheck);
    }
}
