using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShootingDistanceSetting : MonoBehaviour
{
    [SerializeField] private Button buttonAdd;
    [SerializeField] private Button buttonSub;
    [SerializeField] private TextMeshProUGUI distanceIndicator;

    private void Start()
    {
        buttonAdd.onClick.AddListener(() => DistanceChanged(
            ConfigManager.Instance.shootingDistance + 10));
        
        buttonSub.onClick.AddListener(() => DistanceChanged(
            ConfigManager.Instance.shootingDistance - 10));
    }

    private void DistanceChanged(float distance)
    {
        // Wow I discovered "merge into pattern" here :o
        if (distance is < 10 or > 90) { return; }
        
        ConfigManager.Instance.SetShootingDistance((int)distance);
        // 5 : Correction because of the terrain configuration and correcting the terrain
        // isn't a priority
        distanceIndicator.text = $"{5 + (int)distance} m";
    }
}
