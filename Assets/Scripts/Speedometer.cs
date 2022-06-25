using UnityEngine;

public class Speedometer : MonoBehaviour
{
    private const float maxSpeedAngle = -105.5f;
    private const float zeroSpeedAngle = 123.5f;
    private float maxSpeed;
    private float speed;

    [SerializeField] Transform needle;
    [SerializeField] carControllerOnWheels carControllerOnWheelsScript;
    private void Start()
    {
        speed = 0f;
        maxSpeed = carControllerOnWheelsScript.getTopSpeed();
    }

    // Update is called once per frame
    private void Update()
    {
        speed = carControllerOnWheelsScript.getCurrentSpeed();
        needle.eulerAngles = new Vector3(0, 0, getSpeedRotation());
    }

    private float getSpeedRotation()
    {
        float totalAngleSize = zeroSpeedAngle - maxSpeedAngle;
        float speedNormalized = speed / maxSpeed;
        return zeroSpeedAngle - speedNormalized * totalAngleSize;
    }
}
