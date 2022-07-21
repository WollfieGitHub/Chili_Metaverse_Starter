using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointZone : MonoBehaviour
{
    public int Points;
    private HashSet<string> _arrows = new ();
    private Arrow _lastArrow = null;
    public int nbOfArrowsPlanted = 0;

    private PointZones _pointZones;
    
    private void Start()
    {
        _pointZones = GetComponentInParent<PointZones>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Arrow arrow = other.gameObject.GetComponentInParent<Arrow>();
        if (ReferenceEquals(null, arrow)) { return; }

        DebugUI.Show($"Arrow entered zone of score {Points} : {arrow.UID}");
        Rigidbody arrowBody = arrow.GetComponent<Rigidbody>();
        arrowBody.useGravity = false;
        arrowBody.isKinematic = true; // TODO CHECK this
        _arrows.Add(arrow.UID);
        _lastArrow = arrow;
        nbOfArrowsPlanted = _arrows.Count;

        try
        {
            DebugUI.Show("Updating score from Point Zone...");
            UpdateScore(arrow);
        }
        catch (Exception e)
        {
            DebugUI.Show(e.StackTrace);
            DebugUI.Show(e.Message);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Arrow arrow = other.gameObject.GetComponentInParent<Arrow>();
        if (ReferenceEquals(null, arrow)) { return; }

        _arrows.Remove(arrow.UID); 
        if (_lastArrow == arrow)
        {
            _lastArrow = null;
        }
        nbOfArrowsPlanted = _arrows.Count;
    }

    public bool IsArrowPlanted(Arrow arrow)
    {
        return _arrows.Contains(arrow.UID);
    }

    private void UpdateScore(Arrow arrow)
    {
        _pointZones.RequestScoreUpdate(arrow);
    }
}
