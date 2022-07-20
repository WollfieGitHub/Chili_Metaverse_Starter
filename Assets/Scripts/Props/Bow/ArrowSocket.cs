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
        if (ReferenceEquals(null, arrow))
        {
            return;
        }

        Collider arrowCollider = arrow.GetComponent<Collider>();
        arrowCollider.isTrigger = true;
        Rigidbody arrowBody = arrow.GetComponent<Rigidbody>();
        arrowBody.useGravity = false;
        arrowBody.isKinematic = true;
        bowBehavior.SetArrowLoaded(arrow);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        Arrow arrow = args.interactableObject.transform.GetComponent<Arrow>();
        if (ReferenceEquals(null, arrow))
        {
            return;
        }

        Rigidbody arrowBody = arrow.GetComponent<Rigidbody>();
        Collider arrowCollider = arrow.GetComponent<Collider>();
        arrowCollider.isTrigger = true;
        arrowBody.useGravity = true;
        arrowBody.isKinematic = false;
        bowBehavior.UnloadArrow();
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        Arrow arrow = interactable.transform.GetComponent<Arrow>();
        return bowBehavior.isHeld 
               && (!bowBehavior.IsArrowLoaded || bowBehavior.loadedArrowId() == arrow.UID)
               && (!ReferenceEquals(null, arrow) && !arrow.Launched);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        Arrow arrow = interactable.transform.GetComponent<Arrow>();
        return bowBehavior.isHeld
               && (!ReferenceEquals(null, arrow) && !arrow.Launched)
               && (!bowBehavior.IsArrowLoaded || bowBehavior.loadedArrowId() == arrow.UID);
    }


    private void Update()
    {
        Vector3 notchPosition = bowNotchTransform.localPosition;
        Vector3 stringHandlePosition = stringHandleTransform.localPosition;
        Vector3 bowPull = stringHandlePosition - _defaultStringPosition;
        Vector3 arrowPosition = notchPosition + offset + bowPull;
        ArrowDirection = notchPosition - stringHandlePosition;
        Vector3 upDirection = bowNotchTransform.up;
        _transform.localPosition = arrowPosition;
        _transform.localRotation = Quaternion.LookRotation(
             Vector3.Cross(ArrowDirection, upDirection), 
            upDirection
        );
    }

    public void ReleaseArrow()
    {
        if (hasSelection) { interactionManager.SelectExit(this, firstInteractableSelected); }
    }
}
