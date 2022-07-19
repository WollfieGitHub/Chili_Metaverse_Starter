using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformLink : MonoBehaviour
{
    [SerializeField] private Transform attachTo;
    [SerializeField] private Vector3 offset;
    private Transform _transform;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position = attachTo.position + offset;
    }
}
