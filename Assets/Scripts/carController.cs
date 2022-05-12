using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    public Rigidbody sphereRB;
    public float fwdSpeed;
    public float revSpeed;
    public float turnSpeed;
    
    public float dragMin;
    public float dragMax;
    public LayerMask groundLayer;

    private float forwardInput;
    private float turnInput;
    private float actingDrag;
    private bool isGrounded;
    
    
    void Start()
    {
        sphereRB.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = sphereRB.transform.position;

        forwardInput = Input.GetAxisRaw("Vertical");
        forwardInput *= forwardInput > 0 ? fwdSpeed : revSpeed;


        turnInput = Input.GetAxisRaw("Horizontal");
        float newRotation = turnInput * Input.GetAxisRaw("Vertical") * turnSpeed * Time.deltaTime;

        if (isGrounded)
        {
            actingDrag = Input.GetButton("Vertical") ? dragMin : dragMax;
            sphereRB.drag = actingDrag;
            transform.Rotate(0, newRotation, 0, Space.World);

        }

    }
    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, 2f, groundLayer);

        //sphereRB.velocity = transform.forward * forwardInput * Time.fixedDeltaTime;
        if (!isGrounded)
        {
            Debug.Log("Not grounded");
            sphereRB.AddForce(transform.up * -30f, ForceMode.Acceleration);

        }
        else
        {
            Debug.Log("Grounded");
            sphereRB.AddForce(transform.right * forwardInput, ForceMode.Acceleration);
            //sphereRB.velocity = transform.right * forwardInput * Time.fixedDeltaTime * 100;
            
        }
             
        
    }

    
}
