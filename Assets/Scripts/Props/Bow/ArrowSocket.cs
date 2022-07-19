using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowSocket : XRSocketInteractor
{
    [SerializeField] private BowBehavior bowBehavior;

    [SerializeField] private Transform stringHandleTransform;
    [SerializeField] private Transform arrowNotchTransform;

    private Transform _transform;
    
    protected override void Start()
    {
        base.Start();
        _transform = transform;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        bowBehavior.SetArrowLoaded(true);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        bowBehavior.SetArrowLoaded(true);
    }

    private void Update()
    {
        Vector3 notchDirection = (stringHandleTransform.position + arrowNotchTransform.position) / 2.0f;
        _transform.position = notchDirection;
    }
}
