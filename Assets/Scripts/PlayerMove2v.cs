using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2v : MonoBehaviour
{
    public float movementSpeed = 15;
    public float rotationSpeed = 4;
    private Rigidbody rb;
    private float forwardMovement, rotatePlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        forwardMovement = Input.GetAxis("Vertical");
        rotatePlayer = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        //Vector3 transformDirention = transform.TransformDirection(transform.forward * movementSpeed * forwardMovement * Time.deltaTime);
        //rb.MovePosition(rb.position + transform.forward * movementSpeed * forwardMovement * Time.deltaTime);
        rb.MovePosition(rb.position + transform.TransformDirection(transform.forward * movementSpeed * forwardMovement) * Time.deltaTime);
        float turn = rotatePlayer * rotationSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);


    }
}