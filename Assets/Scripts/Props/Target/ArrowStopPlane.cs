using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowStopPlane : MonoBehaviour
{
    [SerializeField] private PointZones pointZones;
    
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Arrow arrow = collision.gameObject.GetComponent<Arrow>();
            StartCoroutine(nameof(UpdateScore), arrow);
        }
    }

    private IEnumerable UpdateScore(Arrow arrow)
    {
        yield return new WaitForSeconds(1f);
        ScoreManager.Instance.NewHit(pointZones.GetNbPointsOf(arrow));
    }
}
