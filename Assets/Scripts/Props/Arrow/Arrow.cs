using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody _rigidbody;
    public bool IsPlacedInQuiver { private set; get; }

    public bool Launched { private set; get; } = false;
    public string UID { private set; get; }

    // Start is called before the first frame update
    void Start()
    {
        UID = "Arrow:" + Environment.TickCount;
        _transform = transform;
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

        Ray ray = new Ray(_transform.position, _transform.forward);
        LayerMask mask = LayerMask.GetMask("IgnoreArrows");
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.6f, ~mask.value))
        {
            DebugUI.Show($"Arrow hit {hit.transform.gameObject.name}");
            ScoreManager.Instance.NewHit(0);
            Launched = false;
            _rigidbody.freezeRotation = false;
            TogglePhysics(false);
        }
    }

    private void TogglePhysics(bool physicsEnabled)
    {
        DebugUI.Show("Arrow Physics toggled to " + physicsEnabled);
        _rigidbody.useGravity = physicsEnabled;
        _rigidbody.isKinematic = !physicsEnabled;
    }

    public void PlaceInQuiver() => IsPlacedInQuiver = true;
    public void RemoveFromQuiver() => IsPlacedInQuiver = false;

    public void SetLaunched() => Launched = true;
}
