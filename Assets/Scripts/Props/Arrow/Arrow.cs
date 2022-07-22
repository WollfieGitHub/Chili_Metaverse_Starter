using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform _transform;
    private Transform _tipTransform;
    private Rigidbody _rigidbody;
    public bool IsPlacedInQuiver { private set; get; }

    public bool Launched { private set; get; }
    public string Uid { private set; get; }

    // Start is called before the first frame update
    void Start()
    {
        Uid = "Arrow:" + Environment.TickCount;
        Launched = false;
        _transform = transform;
        _tipTransform = GameObject.FindWithTag("ArrowTip").transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Launch(float tension)
    {
        DebugUI.Show("Launched !");
        TogglePhysics(true);
        Launched = true;
        _rigidbody.freezeRotation = true;
        _rigidbody.AddForce(tension * ConfigManager.Instance.arrowSpeed * _transform.forward.normalized,
            ForceMode.VelocityChange);
    }

    private void Update()
    {
        if (!Launched) { return; }

        Ray ray = new Ray(_tipTransform.position, _transform.forward);
        LayerMask ignoreMask = LayerMask.GetMask("IgnoreArrows");
        LayerMask zoneMask = LayerMask.GetMask("ArrowZone");
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.6f, ~ignoreMask.value | zoneMask.value))
        {
            PointZone zone = hit.transform.GetComponent<PointZone>();

            TogglePhysics(false);
            Launched = false;
            
            if (!ReferenceEquals(zone, null)) { zone.Caught(this); }
            else { ScoreManager.Instance.NewHit(0); }
            
            DebugUI.Show($"Arrow hit {hit.transform.gameObject.name}");
            _rigidbody.freezeRotation = false;
        }
    }

    public void TogglePhysics(bool physicsEnabled)
    {
        DebugUI.Show("Arrow Physics toggled to " + physicsEnabled);
        _rigidbody.useGravity = physicsEnabled;
        _rigidbody.isKinematic = !physicsEnabled;
    }

    public void PlaceInQuiver() => IsPlacedInQuiver = true;
    public void RemoveFromQuiver() => IsPlacedInQuiver = false;

    public void SetLaunched() => Launched = true;
}
