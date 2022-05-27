using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarMovementScene0 : MonoBehaviour
{
    //public float roadBoundaryRight,roadBoundaryLeft;
    //public float stearingSpeed;
    //private float getInput;
    //private Vector3 position;

    //private void Start()
    //{
    //    position = transform.position;
    //}
    //private void getUserInput()
    //{
    //    getInput = Input.GetAxisRaw("Horizontal");
    //}

    //private void Update()
    //{

    //    getUserInput();
    //    position.x += getInput * Time.deltaTime * stearingSpeed; 
    //    position.x = Mathf.Clamp(position.x, -roadBoundaryLeft, roadBoundaryRight);
    //    transform.position = position;// new Vector3( carMove, transform.position.y, transform.position.z);
    //    //Debug.Log(carMove);
    //}

    public float min;
    public float max;
    public float sensitivity;
    private float userInput;
    private Vector3 carPosition;
    Rigidbody rb;
    private bool checkBoundary;
    private float checkDirection;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        carPosition = transform.position;
    }

    private void Update()
    {
        userInput = Input.GetAxisRaw("Horizontal");
        //carPosition.x += userInput;
        carPosition.x = Mathf.Clamp(transform.position.x, min, max);
        transform.position = carPosition;
        checkBoundary = carPosition.x == min || carPosition.x == max ? true : false;
        checkDirection = userInput;
        
    }

    private void FixedUpdate()
    {
        Vector3 forceToApply = Vector3.right * sensitivity * userInput;
        if (checkBoundary)
        {
            if (checkDirection < 0 && carPosition.x == -3.4f)
                forceToApply = Vector3.zero;

            if (checkDirection > 0 && carPosition.x == 4.6f)
                forceToApply = Vector3.zero;
        }
        //rb.velocity = forceToApply;
        rb.AddForce(forceToApply * Time.fixedDeltaTime, ForceMode.Impulse);
    }




}
