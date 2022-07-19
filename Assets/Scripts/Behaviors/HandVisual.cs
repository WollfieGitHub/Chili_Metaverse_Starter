using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVisual : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Color _defaultColor;
    
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultColor = _meshRenderer.material.color;
    }

    public void OnHoverEntered()
    {
        _meshRenderer.material.color = new Color(1, 1, 1);
    }

    public void OnHoverExited()
    {
        _meshRenderer.material.color = _defaultColor;
    }
}
