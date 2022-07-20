using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowStopPlane : MonoBehaviour
{
    [SerializeField] private PointZones pointZones;
    
    public void OnTriggerEnter(Collider collision)
    {
        Arrow arrow = collision.gameObject.GetComponent<Arrow>();
        if (ReferenceEquals(null, arrow)) { return; }
        
        Debug.Log($"An arrow passed the plane : {arrow.UID}");
        StartCoroutine(UpdateScore(arrow));
    }

    private IEnumerator UpdateScore(Arrow arrow)
    {
        yield return new WaitForSeconds(1f);
        int hitScore = pointZones.GetNbPointsOf(arrow);
        Debug.Log($"The arrow {arrow.UID} scored {hitScore} points !");
        ScoreManager.Instance.NewHit(hitScore);
    }
}
