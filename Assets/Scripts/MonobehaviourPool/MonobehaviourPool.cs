using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonobehaviourPool<T> where T : PoolableMonobehaviour
{
    public T poolableMonobehaviourPrefab = default;

    [HideInInspector] public Queue<T> availableMonobehaviours = new Queue<T>();
    [HideInInspector] public List<T> instantiatedMonobehaviours = new List<T>();

    private T availablePoolable()
    {
        T _newPoolableMonobehaviour;

        if (availableMonobehaviours.Count > 0)
        {
            _newPoolableMonobehaviour = availableMonobehaviours.Dequeue();
        }
        else
        {
            _newPoolableMonobehaviour = InstantiatedPoolableMonobehaviour();
        }

        return _newPoolableMonobehaviour;
    }

    private T InstantiatedPoolableMonobehaviour()
    { 
        T _instantiatedPoolableMonobehaviour = MonoBehaviour.Instantiate(poolableMonobehaviourPrefab);
        instantiatedMonobehaviours.Add(_instantiatedPoolableMonobehaviour);
        _instantiatedPoolableMonobehaviour.onPool += OnPoolItemDisabled;

        return _instantiatedPoolableMonobehaviour;
    }

    private void OnPoolItemDisabled(PoolableMonobehaviour _reference)
    {
        availableMonobehaviours.Enqueue((T)_reference);
    }

    public T InstantiateMonobehaviour()
    {
        T _newPoolable = availablePoolable();

        _newPoolable.gameObject.SetActive(true);
        return _newPoolable;
    }

    public T InstantiateMonobehaviour(Vector3 _position)
    {
        T _newPoolable = availablePoolable();

        _newPoolable.transform.position = _position;

        _newPoolable.gameObject.SetActive(true);
        return _newPoolable;
    }

    public T InstantiateMonobehaviour(Vector3 _position, Quaternion _rotation)
    {
        T _newPoolable = availablePoolable();

        _newPoolable.transform.position = _position;
        _newPoolable.transform.rotation = _rotation;

        _newPoolable.gameObject.SetActive(true);
        return _newPoolable;
    }

    public T InstantiateMonobehaviour(Vector3 _position, Quaternion _rotation, Transform _parent)
    {
        T _newPoolable = availablePoolable();

        _newPoolable.transform.SetParent(_parent);
        _newPoolable.transform.position = _position;
        _newPoolable.transform.rotation = _rotation;

        _newPoolable.gameObject.SetActive(true);
        return _newPoolable;
    }

    public void DisableAll()
    {
        foreach (T _currentInstantiatedMonobehaviour in instantiatedMonobehaviours)
        {
            _currentInstantiatedMonobehaviour.gameObject.SetActive(false);
        }
    }

    public void DestroyAll()
    {
        foreach (T _currentInstantiatedMonobehaviour in instantiatedMonobehaviours)
        {
            MonoBehaviour.Destroy(_currentInstantiatedMonobehaviour);
        }
    }
}


