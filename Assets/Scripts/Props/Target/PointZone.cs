using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointZone : MonoBehaviour
{
    public int Points;
    private HashSet<Arrow> _arrows = new ();
    private Arrow _lastArrow = null;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Arrow arrow = collision.gameObject.GetComponent<Arrow>();
            Rigidbody arrowBody = arrow.GetComponent<Rigidbody>();
            arrowBody.useGravity = false;
            arrowBody.freezeRotation = true;
            arrowBody.isKinematic = false; // TODO CHECK this
            _arrows.Add(arrow);
            _lastArrow = arrow;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            Arrow arrow = other.gameObject.GetComponent<Arrow>();
            _arrows.Remove(arrow);
            if (_lastArrow == arrow)
            {
                _lastArrow = null;
            }
        }
    }

    public bool IsArrowPlanted(Arrow arrow)
    {
        return _arrows.Contains(arrow);
    }
}
