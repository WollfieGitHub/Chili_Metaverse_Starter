using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointZones : MonoBehaviour
{
    private PointZone[] _zoneColliders;

    private void Start()
    {
        _zoneColliders = GetComponentsInChildren<PointZone>();
        Array.Sort(_zoneColliders, (zone1, zone2) => zone1.Points - zone2.Points);
    }

    public int GetNbPointsOf(Arrow arrow)
    {
        // Go from largest zone to smallest
        int nbPoints = 0;
        foreach (PointZone pointZone in _zoneColliders)
        {
            bool isInCurrentZone = pointZone.IsArrowPlanted(arrow);
            Debug.Log($"The arrow {(isInCurrentZone ? "is" : "is not")} in {pointZone.Points} points zone");
            if (!isInCurrentZone) { return nbPoints; }

            nbPoints = pointZone.Points;
        }

        return nbPoints;
    }
}
