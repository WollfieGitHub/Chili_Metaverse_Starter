using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class ArrowFetching : MonoBehaviour
{
    [SerializeField] private bool isShootingOrigin;
    [SerializeField] private Transform shootingAnchorTransform;

    private void Start()
    {
        Button btn = GetComponentInChildren<Button>();
        btn.onClick.AddListener(TpPlayerToArrowFetchingPosition);
    }

    private void TpPlayerToArrowFetchingPosition()
    {
        if (isShootingOrigin)
        {
            FindObjectOfType<XROrigin>().transform.position = ShootingDistanceManager.Instance.GetArrowFetchingPosition();
        }
        else
        {
            FindObjectOfType<XROrigin>().transform.position = shootingAnchorTransform.position;
        }
    }
}
