using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BowType
{
    Recurve = 0, Compound = 1
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
        SetBowType(bowType);
        shootingDistance = Mathf.RoundToInt(shootingDistance / 10.0f) * 10;
    }

    private void Start()
    {
        ShootingDistanceManager.Instance.UpdateDistance();
    }

    public void SetBowType(BowType bowType)
    {
        switch (bowType)
        {
            case BowType.Compound: arrowSpeed = 83.8f; break;
            case BowType.Recurve: arrowSpeed = 69.3f; break;
        }
    }
    
    public void SetShootingDistance(int newShootingDistance)
    {
        shootingDistance = newShootingDistance;
        ShootingDistanceManager.Instance.UpdateDistance();
    }
}
