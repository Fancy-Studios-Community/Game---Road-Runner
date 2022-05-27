using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    public Rigidbody sphereRB;
    public float fwdSpeed;
    public float revSpeed;
    public float accelerationSensitivity = 1f;
    public float turnSpeed;
    public float RaycastHeight = 2.5f;
    public float dragMin;
    public float dragMax;
    public LayerMask groundLayer;

    private float forwardInput;
    private float turnInput;
    private float actingDrag;
    private bool isGrounded;
    private float t = 0;
    private float targetForceInput;

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
        if (Input.GetButtonDown("Vertical"))
        {
            t = 0;
        }

        targetForceInput = returnActingForce((targetForceInput == 0) ? 0 : targetForceInput, forwardInput, t);
        t++;
        if(targetForceInput != 0 && targetForceInput != 200)
        {

        Debug.Log(targetForceInput);
        }

    }
    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, RaycastHeight, groundLayer);

        //sphereRB.velocity = transform.forward * forwardInput * Time.fixedDeltaTime;
        if (!isGrounded)
        {
            Debug.Log("Not grounded");
            sphereRB.AddForce(transform.up * -30f, ForceMode.Acceleration);
            targetForceInput = 0;
        }
        else
        {
            //Debug.Log("Grounded");
            sphereRB.AddForce(transform.right * targetForceInput, ForceMode.Acceleration);
            //sphereRB.velocity = transform.right * forwardInput * Time.fixedDeltaTime * 100;
            
        }
             
        
    }

    public float returnActingForce(float initial,float final,float t)
    {

        float temp = t * 0.1f * accelerationSensitivity * Time.deltaTime;
        return Mathf.Lerp(initial, final, temp);
    }
    


    public Vector3 returnPlayerPostion()
    {
        return transform.position;
    }

    
}
