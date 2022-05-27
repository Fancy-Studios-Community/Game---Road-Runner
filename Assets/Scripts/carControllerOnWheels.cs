using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carControllerOnWheels : MonoBehaviour
{
    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private float steeringInput;
    private bool handbrakes;
    private bool gameActive = true;
    
    public WheelCollider frontLeftCollider, frontRightCollider;
    public WheelCollider rearLeftCollider, rearRightCollider;
    public Transform frontLeftT, frontRightT;
    public Transform rearLeftT, rearRightT;
    public float topSpeed = 300f;
    public float maxSteerAngle = 30f;
    public float motorForce = 50;
    public float COM = 1f;
    public bool frontWheelDrive = true;
    public bool rearWheelDrive = false;
    public float frontWheelForwardStiffness = 1f;
    public float rearWheelForwardStiffness = 1f;
    public float frontWheelSidewaysStiffness = 1f;
    public float rearWheelSidewaysStiffness = 1f;
    public float wheelMass = 50f;
    public GameObject UI;
    public GameObject UIscore;

    public void GetInput()
    {
        if (!gameActive) return;
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        handbrakes = Input.GetKey(KeyCode.Space);
    }

    private void Steer()
    {
        steeringInput = maxSteerAngle * horizontalInput;
        frontLeftCollider.steerAngle = steeringInput;
        frontRightCollider.steerAngle = steeringInput;
    }
    private void Accelerate()
    {
        float topSpeedLimiter = rb.velocity.sqrMagnitude > topSpeed ? 0 : 1;

        if(frontWheelDrive == true & !handbrakes)
        {
        frontLeftCollider.motorTorque = verticalInput * motorForce * topSpeedLimiter;
        frontRightCollider.motorTorque = verticalInput * motorForce * topSpeedLimiter;

        }
        if(rearWheelDrive == true & !handbrakes)
        {
            rearLeftCollider.motorTorque = verticalInput * motorForce * topSpeedLimiter;
            rearRightCollider.motorTorque = verticalInput * motorForce * topSpeedLimiter;
            
        }
    }

    private void updateWheelPoses()
    {
        UpdateWheelPose(frontLeftCollider, frontLeftT,"left");
        UpdateWheelPose(rearLeftCollider, rearLeftT,"left");
        UpdateWheelPose(frontRightCollider, frontRightT,"right");
        UpdateWheelPose(rearRightCollider, rearRightT,"right");
    }

    private void UpdateWheelPose(WheelCollider _collider,Transform _transform,string face)
    {
        Vector3 _pos;
        Quaternion _quat;
        Vector3 faceDirection = face == "left" ? new Vector3(0, 90, 0) : new Vector3(0, -90, 0);

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat * Quaternion.Euler(faceDirection);

    }
    private void handbrake()
    {
        float handBrakesApplied = handbrakes ? 1 : 0;
        rearLeftCollider.brakeTorque = 2000f * handBrakesApplied;
        rearRightCollider.brakeTorque = 2000f * handBrakesApplied;
        frontLeftCollider.brakeTorque = 1000f * handBrakesApplied;
        frontRightCollider.brakeTorque = 1000f * handBrakesApplied;
    }

    private void setStiffness()
    {
        frontLeftCollider.forwardFriction = calStiffness(frontLeftCollider.forwardFriction, frontWheelForwardStiffness);
        frontRightCollider.forwardFriction = calStiffness(frontRightCollider.forwardFriction, frontWheelForwardStiffness);
        rearLeftCollider.forwardFriction = calStiffness(rearLeftCollider.forwardFriction, rearWheelForwardStiffness);
        rearRightCollider.forwardFriction = calStiffness(rearRightCollider.forwardFriction, rearWheelForwardStiffness);
        frontLeftCollider.sidewaysFriction = calStiffness(frontLeftCollider.sidewaysFriction, frontWheelSidewaysStiffness);
        frontRightCollider.sidewaysFriction = calStiffness(frontRightCollider.sidewaysFriction, frontWheelSidewaysStiffness);
        rearLeftCollider.sidewaysFriction = calStiffness(rearLeftCollider.sidewaysFriction, rearWheelSidewaysStiffness);
        rearRightCollider.sidewaysFriction = calStiffness(rearRightCollider.sidewaysFriction, rearWheelSidewaysStiffness);
    }

    private WheelFrictionCurve calStiffness(WheelFrictionCurve curve,float stiffnessValue)
    {
        WheelFrictionCurve frictionCurve = curve;
        frictionCurve.stiffness = stiffnessValue;
        return frictionCurve;
    }

    //custom funtion just for this game
    private void removeParent()
    {
        frontLeftT.parent = null;
        frontRightT.parent = null;
        rearLeftT.parent = null;
        rearRightT.parent = null;
    }
    private void setWheelMass()
    {
        frontLeftCollider.mass = wheelMass;
        frontRightCollider.mass = wheelMass;
        rearLeftCollider.mass = wheelMass;
        rearRightCollider.mass = wheelMass;
    }

    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.up * COM;
        setStiffness();
        removeParent();
        setWheelMass();
    }

    private void Update()
    {
        var ang = transform.localRotation.eulerAngles.z;
        
        if (ang > 50f && ang < 310f)
        {
            gameActive = false;
            UIscore.SetActive(false);
            UI.SetActive(true);
        }
            
    }
    private void FixedUpdate()
    {
        
        GetInput();
        Steer();
        updateWheelPoses();
        handbrake();
        Accelerate();
    }

}


