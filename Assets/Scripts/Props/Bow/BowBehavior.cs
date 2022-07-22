using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using Object = UnityEngine.Object;

public class BowBehavior : XRGrabInteractable
{
    [FormerlySerializedAs("isHold")] public bool isHeld;
    public float bowSpreading;
    public const float DrawLength = 0.508f; // m

    [Header("Bow String Position")]
    [SerializeField] private Transform topStringAttachPoint;
    [SerializeField] private Transform middleStringAttachPoint;
    [SerializeField] private Transform botStringAttachPoint;
    
    [Header("Socket")]
    [SerializeField] private ArrowSocket arrowSocket;
    
    
    private LineRenderer _lineRenderer;
    private Arrow _arrowLoaded = null;
    public bool IsArrowLoaded { private set; get; } = false;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        isHeld = true;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isHeld = false;
    }

    public void LoadArrow(Arrow loaded) {
        _arrowLoaded = loaded;
        IsArrowLoaded = true;
        DebugUI.Show("Loaded Arrow");
    }
    
    

    public void UnloadArrow()
    {
        DebugUI.Show("Arrow unloaded");
        _arrowLoaded = null;
        IsArrowLoaded = false;
    }
    public void SetBowSpreading(float value) => bowSpreading = value;


    private void Update()
    {
        _lineRenderer.SetPositions(new []
        {
            botStringAttachPoint.position,
            middleStringAttachPoint.position, 
            topStringAttachPoint.position
        });
        // DebugUI.Show($"Tension = {bowSpreading}");
    }

    public void Release()
    {
        DebugUI.Show("Trying to release Arrow");
        if (!IsArrowLoaded)
        {
            DebugUI.Show("No Arrow was loaded");
            return; 
        }
        DebugUI.Show("Releasing loaded Arrow");
        StartCoroutine(nameof(ReleaseArrow));
    }

    private void ReleaseArrow()
    {
        DebugUI.Show($"Launching Arrow... {_arrowLoaded}");
        Arrow arrow = _arrowLoaded;
        UnloadArrow();
        try
        {
            arrow.SetLaunched();
            arrowSocket.ReleaseArrow();
        }
        catch (Exception e)
        {
            DebugUI.Show(e.StackTrace);
            DebugUI.Show(e.Message);
        }
        
        StartCoroutine(nameof(LaunchArrow), arrow);
        bowSpreading = 0.0f;
    }

    private void LaunchArrow(Arrow arrow)
    {
        arrow.Launch(bowSpreading / DrawLength );
    }

    public string LoadedArrowId() => ReferenceEquals(_arrowLoaded, null) ? null : _arrowLoaded.Uid;

    public override bool IsSelectableBy(IXRSelectInteractor interactor) => 
        (!isHeld || interactorsSelecting.Contains(interactor)) && base.IsSelectableBy(interactor);
    public override bool IsHoverableBy(IXRHoverInteractor interactor) => 
        (!isHeld || interactorsHovering.Contains(interactor)) && base.IsHoverableBy(interactor);
    
}
