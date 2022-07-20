using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDistanceManager : MonoBehaviour
{
    private List<GameObject> _spawned10MetersPrefabs = new();
    private GameObject _spawnedEnd;
    private Transform _spawnedEndTransform;
    private int _lastDistance = 0;

    [SerializeField] private Vector3 originOffset;
    [SerializeField] private GameObject prefabMiddle;
    [SerializeField] private GameObject prefabEnd;

    private readonly Vector3 _diff10Meters = new (0, 0, -10); 
    
    public static ShootingDistanceManager Instance;
    private Transform _transform;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _transform = transform;
        _spawnedEnd = Instantiate(prefabEnd, _transform);
        _spawnedEndTransform = _spawnedEnd.transform;
    }

    public void UpdateDistance()
    {
        int newShootingDistance = ConfigManager.Instance.shootingDistance;
        Vector3 lastPosition = SpawnMiddlePrefabs(newShootingDistance);
        _spawnedEndTransform.position = lastPosition;
    }

    private Vector3 SpawnMiddlePrefabs(int newShootingDistance)
    {
        Vector3 currentPosition = originOffset + new Vector3(_lastDistance, 0, 0);
        int diff = (newShootingDistance - _lastDistance - 2)/10;
        if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                GameObject new10MetersPrefab = Instantiate(prefabMiddle, _transform);
                new10MetersPrefab.transform.position = currentPosition;
            
                _spawned10MetersPrefabs.Add(new10MetersPrefab);

                currentPosition += _diff10Meters;
            }
        }
        else
        {
            
            for (int i = 0; i < Mathf.Abs(diff); i++)
            {
                GameObject removedPrefab = _spawned10MetersPrefabs[^1];
                _spawned10MetersPrefabs.RemoveAt(_spawned10MetersPrefabs.Count - 1);
                Destroy(removedPrefab);
                
                currentPosition -= _diff10Meters;
            }
        }

        return currentPosition;
    }
}
