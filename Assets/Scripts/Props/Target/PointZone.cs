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

    private void OnTriggerEnter(Collider other)
    {
        Arrow arrow = other.gameObject.GetComponent<Arrow>();
        if (ReferenceEquals(null, arrow)) { return; }

        Debug.Log($"Arrow entered zone of score {Points} : {arrow.UID}");
        Rigidbody arrowBody = arrow.GetComponent<Rigidbody>();
        arrowBody.useGravity = false;
        arrowBody.freezeRotation = true;
        arrowBody.isKinematic = false; // TODO CHECK this
        _arrows.Add(arrow.UID);
        _lastArrow = arrow;
        nbOfArrowsPlanted = _arrows.Count;
    }


    private void OnTriggerExit(Collider other)
    {
        Arrow arrow = other.gameObject.GetComponent<Arrow>();
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
}
