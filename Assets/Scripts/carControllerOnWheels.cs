using UnityEngine;
using TMPro;


public class carControllerOnWheels : MonoBehaviour
{
    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private float steeringInput;
    private bool handbrakes;
    private bool gameActive = true;
    private Transform frontLeftT, frontRightT;
    private Transform rearLeftT, rearRightT;
    private collisionTrigger collisionTriggerScript;

    [SerializeField] WheelCollider frontLeftCollider, frontRightCollider;
    [SerializeField] WheelCollider rearLeftCollider, rearRightCollider;
    [SerializeField] GameObject wheel;
    [SerializeField] float topSpeed = 1500.0f;
    [SerializeField] float maxSteerAngle = 20f;
    [SerializeField] float motorForce = 2000.0f;
    [SerializeField] float COM = -3f;
    [SerializeField] bool frontWheelDrive = true;
    [SerializeField] bool rearWheelDrive = true;
    [SerializeField] float frontWheelForwardStiffness = 3f;
    [SerializeField] float rearWheelForwardStiffness = 3f;
    [SerializeField] float frontWheelSidewaysStiffness = 3f;
    [SerializeField] float rearWheelSidewaysStiffness = 3f;
    [SerializeField] float wheelMass = 30f;
    [SerializeField] float spring = 35000f;
    [SerializeField] float damper = 2000f;
    [SerializeField] float targetPosition = 0.4f;
    [SerializeField] float advancedWheelRotation;
    [SerializeField] float permittedTiltBeforeGameOver = 50.0f;
    private float carTiltAngle;
    private bool headlightStatus = false;

    //make them organised
    [SerializeField] bool ifplayingderby;
    [SerializeField] ScoreAndOtherThings scoreAndOtherThings;
    [SerializeField] ScoreAndOtherThings OnEnemyCarscoreAndOtherThings;
    [SerializeField] TMP_Text text;
    [SerializeField] buttonTouchControl brakes;
    [SerializeField] Joystick joystick;
    [SerializeField] float tiltSensitivity;
    //private float frameConstant;
    //private float steerFrameConstant;
    private void GetInput()
    {
        if (!gameActive)
        {
            handbrakes = true;
            return;
        }
        horizontalInput = Input.GetAxis("Horizontal");// * Time.deltaTime * 30f;
        verticalInput = Input.GetAxisRaw("Vertical");// * Time.deltaTime * 30f; 
        handbrakes = Input.GetKey(KeyCode.Space);
        if (Input.GetKeyDown(KeyCode.L))
            toggleLight();
    }

    private void joystickControls()
    {
        if (!gameActive)
        {
            handbrakes = true;
            return;
        }

        verticalInput = joystick.Vertical;
        horizontalInput = joystick.Horizontal;
    }

    private void joystickHandbrake()
    {
        if (!gameActive)
        {
            handbrakes = true;
            return;
        }
        handbrakes = brakes.buttonPressed;
        //Debug.Log(handbrakes);
    }


    private void toggleLight()
    {
        if (headlightStatus == false)
        {
            headlightStatus = true;
            return;
        }
        if (headlightStatus == true)
        {
            headlightStatus = false;
            return;
        }
    }
    private void Steer()
    {
        steeringInput = maxSteerAngle * horizontalInput;
        frontLeftCollider.steerAngle = steeringInput;// * steerFrameConstant;
        frontRightCollider.steerAngle = steeringInput;// * steerFrameConstant;
        //Debug.Log(horizontalInput);
    }
    private void Accelerate()
    {
        float topSpeedLimiter = rb.velocity.sqrMagnitude > topSpeed ? 0 : 1;

        if(frontWheelDrive == true & !handbrakes)
        {
            frontLeftCollider.motorTorque = verticalInput * motorForce * topSpeedLimiter;// * frameConstant;
            frontRightCollider.motorTorque = verticalInput * motorForce * topSpeedLimiter;// * frameConstant;
            
        }
        if(rearWheelDrive == true & !handbrakes)
        {
            rearLeftCollider.motorTorque = verticalInput * motorForce * topSpeedLimiter;// * frameConstant;
            rearRightCollider.motorTorque = verticalInput * motorForce * topSpeedLimiter;// * frameConstant;
            
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
        Vector3 faceDirection = face == "left" ? new Vector3(0, advancedWheelRotation, 0) : new Vector3(0, -advancedWheelRotation, 0);

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

    private void springAndDamping()
    {
        JointSpring suspensionSpring = new JointSpring();
        suspensionSpring.spring = spring;
        suspensionSpring.damper = damper;
        suspensionSpring.targetPosition = targetPosition;
        frontLeftCollider.suspensionSpring = suspensionSpring;
        frontRightCollider.suspensionSpring = suspensionSpring;
        rearLeftCollider.suspensionSpring = suspensionSpring;
        rearRightCollider.suspensionSpring = suspensionSpring;
    }
    private void placeTires()
    {

        frontLeftT = Instantiate(wheel, frontLeftCollider.transform.position, Quaternion.identity, frontLeftCollider.transform).transform;
        frontRightT = Instantiate(wheel, frontRightCollider.transform.position, Quaternion.identity, frontRightCollider.transform).transform;
        rearLeftT = Instantiate(wheel, rearLeftCollider.transform.position, Quaternion.identity, rearLeftCollider.transform).transform;
        rearRightT = Instantiate(wheel, rearRightCollider.transform.position, Quaternion.identity, rearRightCollider.transform).transform;

    }

    private WheelFrictionCurve calStiffness(WheelFrictionCurve curve,float stiffnessValue)
    {
        WheelFrictionCurve frictionCurve = curve;
        frictionCurve.stiffness = stiffnessValue;
        return frictionCurve;
    }

    public bool gameStatus() //called by manageScene script
    {
        return gameActive;
    }

    public float getCurrentSpeed() //called by speedometer script
    {
        if (rb.velocity.sqrMagnitude > topSpeed)
            return topSpeed;
        else
            return rb.velocity.sqrMagnitude;
    }
    public float getTopSpeed() //called by speedometer script
    {
        return topSpeed;
    }

    public bool[] lights()
    {
        bool[] temp = { handbrakes, headlightStatus };
        return temp;
    }

    private void TiltAngle() 
    {
        carTiltAngle = transform.localRotation.eulerAngles.z;
        if (carTiltAngle > permittedTiltBeforeGameOver && carTiltAngle < 360f - permittedTiltBeforeGameOver)
        {
            gameActive = false;

        }
    }

    private void setWheelMass()
    {
        frontLeftCollider.mass = wheelMass;
        frontRightCollider.mass = wheelMass;
        rearLeftCollider.mass = wheelMass;
        rearRightCollider.mass = wheelMass;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collisionTriggerScript = GetComponent<collisionTrigger>();
    }

    private void Start()
    {
        rb.centerOfMass = Vector3.up * COM;
        placeTires();
        setStiffness();
        setWheelMass();
        springAndDamping();
        //frameConstant = Time.deltaTime * 40f;
        //steerFrameConstant = Time.deltaTime * 60f;
    }
    
    private void Update()
    {

        GetInput(); // <-- enable for pc disable for mobile devices
        //joystickControls();
        //joystickHandbrake();

        Steer();
        
        if (collisionTriggerScript.getCarHealth() < 3) gameActive = false;
        
        if (!ifplayingderby)
        {
            TiltAngle();

            if(OnEnemyCarscoreAndOtherThings.getTotalDistance() - scoreAndOtherThings.getTotalDistance() > 500)
            {
                gameActive = false;
                text.text = "HE GOT AWAY...";

            }
        }

        
               
    }
    private void FixedUpdate()
    {        
        updateWheelPoses();
        handbrake();
        if (gameActive) Accelerate();
    }

}


