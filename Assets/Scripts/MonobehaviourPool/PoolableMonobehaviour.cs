using UnityEngine;

public abstract class PoolableMonobehaviour : MonoBehaviour
{
    [HideInInspector] public bool isNew = true;

    public delegate void OnPool(PoolableMonobehaviour _reference);
    public OnPool onPool;

    protected abstract void OnEnable();

    protected virtual void OnDisable()
    {
        if (isNew) isNew = false;
        if (onPool != null) onPool(this);
    }
}
