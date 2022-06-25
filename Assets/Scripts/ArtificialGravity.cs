using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialGravity : MonoBehaviour
{
    [SerializeField] Transform planetTransform;
    [SerializeField] float gravity;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void artificialGravity()
    {
        
        Vector3 gravityUp = (rb.position - planetTransform.position).normalized;
        Vector3 localUp = rb.transform.up;
        rb.AddForce(gravityUp * -gravity);
        rb.rotation = Quaternion.FromToRotation(localUp, gravityUp) * rb.rotation;
    }

    
    private void FixedUpdate()
    {
        artificialGravity();
    }
}
