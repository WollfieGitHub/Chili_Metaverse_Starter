using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class QuiverArrowSocket : XRSocketInteractor
{
    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        Arrow arrow = args.interactableObject.transform.GetComponent<Arrow>();
        // We know arrow is not null from CanHover and CanSelect implementations
        arrow.RemoveFromQuiver();
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        Arrow arrow = interactable.transform.GetComponent<Arrow>();
        return !hasSelection && !ReferenceEquals(null, arrow) && !arrow.IsPlacedInQuiver;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        Arrow arrow = interactable.transform.GetComponent<Arrow>();
        return !hasSelection && !ReferenceEquals(null, arrow) && !arrow.IsPlacedInQuiver;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Arrow arrow = args.interactableObject.transform.GetComponent<Arrow>();
        // We know arrow is not null from CanHover and CanSelect implementations
        arrow.PlaceInQuiver();
    }
}
