using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowBehavior : XRGrabInteractable
{
    public bool isHold;
    public bool isArrowLoaded;
    public double bowSpreading;

    [Header("Bow String Position")]
    [SerializeField] private Transform topStringAttachPoint;
    [SerializeField] private Transform middleStringAttachPoint;
    [SerializeField] private Transform botStringAttachPoint;

    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void OnHandleGrabbed()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        isHold = true;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isHold = false;
    }

    public void SetArrowLoaded(bool loaded) => isArrowLoaded = loaded;
    public void SetBowSpreading(double value) => bowSpreading = value;

    private void Update()
    {
        _lineRenderer.SetPositions(new []
        {
            botStringAttachPoint.position,
            middleStringAttachPoint.position, 
            topStringAttachPoint.position
        });
    }
}
