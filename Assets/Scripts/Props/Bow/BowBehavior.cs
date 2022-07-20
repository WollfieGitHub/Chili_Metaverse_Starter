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

    private ArrowSocket _arrowSocket;
    
    private LineRenderer _lineRenderer;
    private Arrow _arrowLoaded = null;
    public bool IsArrowLoaded { private set; get; } = false;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _arrowSocket = GetComponentInChildren<ArrowSocket>();
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

    public void SetArrowLoaded(Arrow loaded) {
        _arrowLoaded = loaded;
        IsArrowLoaded = true;
    }

    public void UnloadArrow()
    {
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
        
        DebugUI.Show(
            $"Loaded : {_arrowLoaded}" +
            $"\nDistance : {bowSpreading}" +
            $"\nHeld : {isHeld}");
    }

    public void Release()
    {
        if (ReferenceEquals(_arrowLoaded, null)) { return; }

        StartCoroutine(nameof(ReleaseArrow));
    }

    private void ReleaseArrow()
    {
        _arrowSocket.ReleaseArrow();
        _arrowLoaded.Launch(bowSpreading / DrawLength );
        bowSpreading = 0.0f;
    }

    public string loadedArrowId() => !ReferenceEquals(_arrowLoaded, null) ? null : _arrowLoaded.UID;
}
