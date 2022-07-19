using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowStringHandle : XRGrabInteractable
{
    [Header("Bow String Handle")]
    [SerializeField] private BowBehavior bowBehavior;
    private Transform _transform;
    private Transform _attachTransform;
    
    private Vector3 _currentPosition;
    private Vector3 _defaultPosition;
    
    private bool _selected = false;
    
    protected override void Awake()
    {
        base.Awake();
        _transform = transform;
        _attachTransform = attachTransform;
        _defaultPosition = _transform.localPosition;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return true;
    }

    private void Update()
    {
        if (!_selected)
        {
            
        }
        else
        {
            _currentPosition = _transform.localPosition;
            bowBehavior.SetBowSpreading(Math.Abs((_defaultPosition - _currentPosition).magnitude));
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        _selected = true;

        bowBehavior.OnHandleGrabbed();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        _selected = false;
        
        // Reset back to default
        bowBehavior.SetBowSpreading(0.0);
        _transform.position = _defaultPosition;
    }
}
