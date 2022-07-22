using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowSocket : XRSocketInteractor
{
    [Header("Bow Transforms")] [SerializeField]
    private BowBehavior bowBehavior;

    [SerializeField] private Transform stringHandleTransform;
    [SerializeField] private Transform bowNotchTransform;
    [SerializeField] private Vector3 offset;
    private Vector3 _defaultStringPosition;
    public Vector3 ArrowDirection { private set; get; }

    private Transform _transform;

    protected override void Start()
    {
        base.Start();
        _transform = attachTransform;
        _defaultStringPosition = stringHandleTransform.localPosition;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Arrow arrow = args.interactableObject.transform.GetComponent<Arrow>();
        DebugUI.Show($"Selected {arrow}");
        if (ReferenceEquals(null, arrow)) { return; }
        DebugUI.Show($"Selected arrow wasn't null : {arrow.Uid}");
        
        Rigidbody arrowBody = arrow.GetComponent<Rigidbody>();
        arrowBody.useGravity = false;
        arrowBody.isKinematic = true;
        DebugUI.Show("Load arrow called");
        bowBehavior.LoadArrow(arrow);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        Arrow arrow = args.interactableObject.transform.GetComponent<Arrow>();
        if (ReferenceEquals(null, arrow)) { return; }
        if (arrow.Launched) { return; }

        Rigidbody arrowBody = arrow.GetComponent<Rigidbody>();
        arrowBody.useGravity = true;
        arrowBody.isKinematic = false;
        bowBehavior.UnloadArrow();
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        Arrow arrow = interactable.transform.GetComponent<Arrow>();
        return base.CanHover(interactable)
               && bowBehavior.isHeld
               && (!bowBehavior.IsArrowLoaded || bowBehavior.LoadedArrowId() == arrow.Uid)
               && (!ReferenceEquals(null, arrow) && !arrow.Launched);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        Arrow arrow = interactable.transform.GetComponent<Arrow>();
        return base.CanSelect(interactable)
               && bowBehavior.isHeld
               && !ReferenceEquals(null, arrow) && !arrow.Launched
               && (!bowBehavior.IsArrowLoaded || bowBehavior.LoadedArrowId() == arrow.Uid);
    }


    private void Update()
    {
        Vector3 notchPosition = bowNotchTransform.localPosition;
        Vector3 stringHandlePosition = stringHandleTransform.localPosition;
        Vector3 bowPull = _defaultStringPosition - stringHandlePosition;
        Vector3 arrowPosition = notchPosition + offset - bowPull;
        ArrowDirection = notchPosition - stringHandlePosition;
        _transform.localPosition = arrowPosition;
        _transform.localRotation = Quaternion.LookRotation(
             ArrowDirection, 
            Vector3.Cross(bowNotchTransform.up, ArrowDirection)
        );
    }

    public void ReleaseArrow()
    {
        if (hasSelection) { interactionManager.SelectExit(this, firstInteractableSelected); }
        else { DebugUI.Show("Socket had no selection");}
    }
}
