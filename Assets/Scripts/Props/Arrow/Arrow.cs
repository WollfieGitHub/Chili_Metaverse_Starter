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

    public bool Launched { private set; get; } = false;
    public string Uid { private set; get; }

    // Start is called before the first frame update
    void Start()
    {
        Uid = "Arrow:" + Environment.TickCount;
        _transform = transform;
        _tipTransform = GameObject.FindWithTag("ArrowTip").transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Launch(float tension)
    {
        DebugUI.Show("Launched !");
        TogglePhysics(true);
        try
        {
            Launched = true;
            _rigidbody.freezeRotation = true;
            _rigidbody.AddForce(tension * ConfigManager.Instance.arrowSpeed * _transform.forward.normalized,
                ForceMode.VelocityChange);
        }
        catch (Exception e)
        {
            DebugUI.Show(e.StackTrace);
            DebugUI.Show(e.Message);
        }
    }

    private void Update()
    {
        if (!Launched) { return; }

        Ray ray = new Ray(_tipTransform.position, _transform.forward);
        
        LayerMask ignoreMask = LayerMask.GetMask("IgnoreArrows");
        int mask = ~ignoreMask.value;
        
        if (Physics.Raycast(ray, out RaycastHit hit, 0.1f, mask))
        {
            DebugUI.Show($"Mask : {mask}");
            DebugUI.Show($"Arrow hit {hit.transform.gameObject.name}");
            PointZone zone = hit.transform.GetComponent<PointZone>();
            DebugUI.Show("Passed here Arrow:49");

            TogglePhysics(false);
            Launched = false;

            if (!ReferenceEquals(zone, null)) { zone.Caught(this); }
            else { ScoreManager.Instance.NewHit(0); }

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
