using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointZone : MonoBehaviour
{
    public int Points;
    private HashSet<string> _arrows = new ();
    private Arrow _lastArrow = null;

    private PointZones _pointZones;
    
    private void Start()
    {
        _pointZones = GetComponentInParent<PointZones>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Arrow arrow = other.gameObject.GetComponentInParent<Arrow>();
        if (ReferenceEquals(null, arrow)) { return; }
        
        arrow.TogglePhysics(false);
        Caught(arrow);
    }


    private void OnTriggerExit(Collider other)
    {
        Arrow arrow = other.gameObject.GetComponentInParent<Arrow>();
        if (ReferenceEquals(null, arrow)) { return; }

        _arrows.Remove(arrow.Uid); 
        if (_lastArrow == arrow)
        {
            _lastArrow = null;
        }
    }

    public bool IsArrowPlanted(Arrow arrow)
    {
        return _arrows.Contains(arrow.Uid);
    }

    private void UpdateScore(Arrow arrow)
    {
        _pointZones.RequestScoreUpdate(arrow);
    }

    public void Caught(Arrow arrow)
    {
        _arrows.Add(arrow.Uid);
        _lastArrow = arrow;
        DebugUI.Show($"Arrow entered zone of score {Points} : {arrow.Uid}");

        try
        {
            DebugUI.Show("Updating score from Point Zone...");
            StartCoroutine(nameof(UpdateScore), arrow);
        }
        catch (Exception e)
        {
            DebugUI.Show(e.StackTrace);
            DebugUI.Show(e.Message);
        }
    }
}
