using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDistanceManager : MonoBehaviour
{
    private Stack<GameObject> _spawned10MetersPrefabs = new();
    private GameObject _spawnedEnd;
    private Transform _spawnedEndTransform;
    private int _lastDistance = 0;
    private Vector3 _lastPosition;
    private Vector3 _arrowFetchingPosition;

    [SerializeField] private Vector3 originOffset;
    [SerializeField] private GameObject prefabMiddle;
    [SerializeField] private GameObject prefabEnd;
    
    private bool _endPrefabInstantiated;

    private readonly Vector3 _diff10Meters = new (0, 0.01f, -10f); 
    
    public static ShootingDistanceManager Instance;
    private Transform _transform;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void UpdateDistance()
    {
        _transform = transform;
        if (!_endPrefabInstantiated)
        {
            _spawnedEnd = Instantiate(prefabEnd, _transform);
            _spawnedEndTransform = _spawnedEnd.transform;
            _endPrefabInstantiated = true;
            _lastPosition = originOffset;
        }
        
        int newShootingDistance = ConfigManager.Instance.shootingDistance;
        _lastPosition = SpawnMiddlePrefabs(newShootingDistance);
        _spawnedEndTransform.position = _lastPosition + new Vector3(0, 0, 0.5f);
        _arrowFetchingPosition = _lastPosition + new Vector3(0, 0, -2);
        _lastDistance = newShootingDistance;
    }

    private Vector3 SpawnMiddlePrefabs(int newShootingDistance)
    {
        Vector3 currentPosition = _lastPosition;
        int diff = (newShootingDistance - _lastDistance)/10;
        if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                GameObject new10MetersPrefab = Instantiate(prefabMiddle, _transform);
                new10MetersPrefab.transform.position = currentPosition;
            
                _spawned10MetersPrefabs.Push(new10MetersPrefab);

                currentPosition += _diff10Meters;
            }
        }
        else if (diff < 0)
        {
            try
            {
                for (int i = 0; i < Mathf.Abs(diff); i++)
                {
                    GameObject removedPrefab = _spawned10MetersPrefabs.Pop();
                    DestroyImmediate(removedPrefab);

                    currentPosition -= _diff10Meters;
                }
            } catch (Exception e)
            {
                DebugUI.Show(e.StackTrace);
                DebugUI.Show(e.Message);
            }
        }
        return currentPosition;
    }

    public Vector3 GetArrowFetchingPosition()
    {
        return _arrowFetchingPosition;
    }
}
