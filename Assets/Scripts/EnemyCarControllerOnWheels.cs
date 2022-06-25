using UnityEngine;
using PathCreation;
public class EnemyCarControllerOnWheels : MonoBehaviour
{
    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private float steeringInput;
    private Transform frontLeftT, frontRightT;
    private Transform rearLeftT, rearRightT;
        
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
    [SerializeField] float advancedWheelRotaionSetting = 0f;
    [SerializeField] float wheelMass = 30f;
    [SerializeField] float spring = 35000f;
    [SerializeField] float damper = 2000f;
    [SerializeField] float targetPosition = 0.4f;
    [SerializeField] Transform terrainTransform;
    [SerializeField] PathCreator pathCreator;
    [SerializeField] float distanceBtwPoints;
    [SerializeField] carControllerOnWheels carControllerOnWheelsScript;
    //private ScoreAndOtherThings scoreAndOtherThings;
    //[SerializeField] ScoreAndOtherThings changethisASAP; //move score and other things script to player gameobject
    private Vector3 targetRelativePosition;
    private float distance;
    private float t = 0;
    [SerializeField] bool ifPlayingDerby = true;
    [SerializeField] Transform playerCar;
    private bool wallHit = false;
    //private float frameConstant;
    //private float steerFrameConstant;

    private void GetInput(float forward,float turn)
    {
        horizontalInput = turn;// * Time.deltaTime * 30f;
        verticalInput = forward;// * Time.deltaTime * 30f;
        
    }
    
    private void Steer()
    {
        steeringInput = maxSteerAngle * horizontalInput;
        frontLeftCollider.steerAngle = steeringInput;// * steerFrameConstant;
        frontRightCollider.steerAngle = steeringInput;// * steerFrameConstant;
        
    }
    private void Accelerate()
    {
        float topSpeedLimiter = rb.velocity.sqrMagnitude > topSpeed ? 0 : 1;

        if(frontWheelDrive == true)
        {
            frontLeftCollider.motorTorque = verticalInput * motorForce * topSpeedLimiter;// * frameConstant;
            frontRightCollider.motorTorque = verticalInput * motorForce * topSpeedLimiter;// * frameConstant;

        }
        if(rearWheelDrive == true)
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
        Vector3 faceDirection = face == "left" ? new Vector3(0, -advancedWheelRotaionSetting, 0) : new Vector3(0, advancedWheelRotaionSetting, 0);

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat * Quaternion.Euler(faceDirection);

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

    
    private void setWheelMass()
    {
        frontLeftCollider.mass = wheelMass;
        frontRightCollider.mass = wheelMass;
        rearLeftCollider.mass = wheelMass;
        rearRightCollider.mass = wheelMass;
    }

    private void carMovementAI() // this method takes pathcreator points and drives along that path
    {
        
        
        float forwardAmount = 0f;
        float turnAmount = 0f;

        float reachedTargetDistance = 15f;
        Vector3 enemyCarCurrentRelativePosition = terrainTransform.InverseTransformPoint(transform.position);
        float distanceToTarget = Vector3.Distance(enemyCarCurrentRelativePosition, targetRelativePosition);
        if (distanceToTarget > reachedTargetDistance)
        {
            // Still too far, keep going
            Vector3 dirToMovePosition = (targetRelativePosition - enemyCarCurrentRelativePosition).normalized;
            float dot = Vector3.Dot(transform.forward, dirToMovePosition);

            if (dot > 0) forwardAmount = 1f;

            float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);

            t += 0.5f;
            if (angleToDir > 1f)
            {
                turnAmount = Mathf.Lerp(0, 1, t / 2f);
            }
            if (angleToDir < -1f)
            {
                turnAmount = Mathf.Lerp(0, -1, t / 2f);
            }
            
        }
        else
        {
            distance += distanceBtwPoints;
            Vector3 modifiedTargetPosition = pathCreator.path.GetPointAtDistance(distance);
            targetRelativePosition = new Vector3( modifiedTargetPosition.x,transform.position.y,modifiedTargetPosition.z); 
            t = 0f;
        }
        GetInput(forwardAmount, turnAmount);

        //if (scoreAndOtherThings.getTotalDistance() - changethisASAP.getTotalDistance() > 900)
        //{
        //    //Debug.Log(scoreAndOtherThings.getTotalDistance() - changethisASAP.getTotalDistance());
        //    carControllerOnWheelsScript.gameStatus()
        //}

    }

    private void carMovementAIKiller() //this AI method tracks the player and tries to crash into player
    {
        float forwardAmount = 1f;
        float turnAmount = 0f;
        //bool frontHit = false;
        //bool backHit = false;
        targetRelativePosition = playerCar.position;
        Vector3 dirToMovePosition = (targetRelativePosition - transform.position).normalized;
        float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);

        if (angleToDir > 10f)
        {
            turnAmount = 1f;
        }
        if (angleToDir < -10f)
        {
            turnAmount = -1f;
        }
        /*
        //raycast to check if back of vehical is not hitting wall
        Ray rayBackward = new Ray(transform.position, -transform.forward);
        RaycastHit hitInfoInBackwardDir;
        if (Physics.Raycast(rayBackward, out hitInfoInBackwardDir, 50) && wallHit)
        {
            //hitting wall at max distance 10m away
            Debug.DrawLine(rayBackward.origin, hitInfoInBackwardDir.point, Color.green);
            if (hitInfoInBackwardDir.transform.tag == "wall")
            {
                //confirm it's a wall
                Debug.Log("hitting wall at rear");
                
            }
        }
        else wallHit = false;
        */
        //raycast to check if front of vehical is not hitting wall
        Ray rayForward = new Ray(transform.position, transform.forward);
        RaycastHit hitInfoInForwardDir;
        if (Physics.Raycast(rayForward, out hitInfoInForwardDir, 50) && wallHit)
        {
            //hitting wall at max distance 20m away
            Debug.DrawLine(rayForward.origin, hitInfoInForwardDir.point, Color.red);
            if (hitInfoInForwardDir.transform.tag == "wall")
            {
                //confirm it's a wall
                Debug.Log("hitting wall at front");
                forwardAmount = -forwardAmount;
                turnAmount = -turnAmount;
            }
        }
        else wallHit = false;
        /*
        if (wallHit) 
        {
            float wallDistanceFromCar = Vector3.Distance(transform.position, wallPosition);
            
            if (wallDistanceFromCar < 40)
            {
                //distance btw car and wall
                if (frontHit)
                {
                    forwardAmount = -forwardAmount;
                    turnAmount = -turnAmount;
                }else if (backHit)
                {
                    //back is hit
                    //do nothing
                }
                
                
            }
            else 
            {
                
                wallHit = false;
                Debug.Log(wallHit);
            }
        }
        */

        
        GetInput(forwardAmount, turnAmount);
    }

    
    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        
    }

    private void Start()
    {
        rb.centerOfMass = Vector3.up * COM;
        placeTires();
        setStiffness();
        setWheelMass();
        springAndDamping();
        if (ifPlayingDerby) targetRelativePosition = playerCar.position; 
        else targetRelativePosition = pathCreator.path.GetPointAtDistance(2);

        //frameConstant = Time.deltaTime * 60;
        //steerFrameConstant = Time.deltaTime * 60f;
    }
    
    private void Update()
    {
        if (ifPlayingDerby) carMovementAIKiller(); 
        else carMovementAI();

        if (carControllerOnWheelsScript.gameStatus()) Steer();
        


    }
    private void FixedUpdate()
    {        
        updateWheelPoses();
        if (carControllerOnWheelsScript.gameStatus()) Accelerate();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "wall")
        {
            wallHit = true;
            //wallPosition = transform.position;
            //Debug.Log(other.gameObject.tag);
        }
    }

}


