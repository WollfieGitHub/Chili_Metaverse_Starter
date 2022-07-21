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

    private Dictionary<string, bool> _scoreUpdateRequestedFor = new ();
    
    public void RequestScoreUpdate(Arrow arrow)
    {
        if (!_scoreUpdateRequestedFor.ContainsKey(arrow.UID) || !_scoreUpdateRequestedFor[arrow.UID])
        {
            DebugUI.Show($"Requested update for {arrow.UID}");
            _scoreUpdateRequestedFor.Add(arrow.UID, true);
            StartCoroutine(UpdateScore(arrow));
        }
        else
        {
            DebugUI.Show($"A request has already been issued for arrow {arrow.UID}");
        }
    }

    private IEnumerator UpdateScore(Arrow arrow)
    {
        DebugUI.Show("Updating score in 0.5f seconds");
        yield return new WaitForSeconds(0.5f);

        int hitScore = GetNbPointsOf(arrow);
        Debug.Log($"The arrow {arrow.UID} scored {hitScore} points !");
        ScoreManager.Instance.NewHit(hitScore);
        _scoreUpdateRequestedFor.Add(arrow.UID, false);
    }
}
