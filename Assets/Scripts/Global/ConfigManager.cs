using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BowType
{
    RECURVE, COMPOUND
}

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance;

    public BowType bowType;
    public float arrowSpeed;
    public int shootingDistance;

    private void Awake()
    {
        Instance = this;
        switch (bowType)
        {
            case BowType.COMPOUND: arrowSpeed = 83.8f; break;
            case BowType.RECURVE: arrowSpeed = 69.3f; break;
        }

        shootingDistance = Mathf.RoundToInt(shootingDistance / 10.0f) * 10;
    }

    private void Start()
    {
        ShootingDistanceManager.Instance.UpdateDistance();
    }

    public void SetShootingDistance(int newShootingDistance)
    {
        shootingDistance = newShootingDistance;
        ShootingDistanceManager.Instance.UpdateDistance();
    }
}
