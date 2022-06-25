using UnityEngine;
using System.Collections.Generic;

public class carSpecialEffects : MonoBehaviour
{
    
    private WheelCollider wheelFrontRight;
    private WheelCollider wheelFrontLeft;
    private WheelCollider wheelRearRight;
    private WheelCollider wheelRearLeft;
    [SerializeField] TrailRenderer trailFrontRight;
    [SerializeField] TrailRenderer trailFrontLeft;
    [SerializeField] TrailRenderer trailRearRight;
    [SerializeField] TrailRenderer trailRearLeft;
    [SerializeField] float skidTolerence = 0.1f;
    [SerializeField] Light headLightL;
    [SerializeField] Light headLightR;
    [SerializeField] Material brakeLightOn;
    [SerializeField] Material brakeLightOff;
    [SerializeField] Renderer brakeLightObject1;
    [SerializeField] Renderer brakeLightObject2;
    [SerializeField] List<GameObject> carParts = new List<GameObject>();
    [SerializeField] ParticleSystem smoke;
    [SerializeField] ParticleSystem darkSmoke;
    [SerializeField] bool collisionScriptUsed = true;
    private collisionTrigger collisionTriggerScript;
    private carControllerOnWheels CarControllerOnWheelsScript;

    private void checkLatSlip(WheelCollider wheel,TrailRenderer trail)
    {
        wheel.GetGroundHit(out WheelHit wheelData);
        float slipLat = wheelData.sidewaysSlip;
        if (Mathf.Abs(slipLat) > skidTolerence)
        {
            trail.emitting = true;
        }
        else trail.emitting = false;
    }

    private void getWheelParent()
    {
        wheelFrontLeft = trailFrontLeft.GetComponentInParent<WheelCollider>();
        wheelFrontRight = trailFrontRight.GetComponentInParent<WheelCollider>();
        wheelRearLeft = trailRearLeft.GetComponentInParent<WheelCollider>();
        wheelRearRight = trailRearRight.GetComponentInParent<WheelCollider>();
        
    }
    private void Start()
    {
        if (collisionScriptUsed)
        {
            CarControllerOnWheelsScript = GetComponent<carControllerOnWheels>();
            collisionTriggerScript = GetComponent<collisionTrigger>();
        }
        getWheelParent();
        
    }

    private void Update()
    {
        checkLatSlip(wheelFrontLeft,trailFrontLeft);
        checkLatSlip(wheelFrontRight, trailFrontRight);
        checkLatSlip(wheelRearLeft, trailRearLeft);
        checkLatSlip(wheelRearRight, trailRearRight);
        if (collisionScriptUsed)
        {
            carHealthEffect(collisionTriggerScript.getCarHealth());
            bool handbrakes = CarControllerOnWheelsScript.lights()[0];
            float headlightStatus = CarControllerOnWheelsScript.lights()[1] ? 1 : 0;
            lights(handbrakes, headlightStatus);
        }
    }

    private void lights(bool rearLight,float headLightStatus)  
    {
        Material[] tailLightL;
        Material[] tailLightR;
        tailLightL = brakeLightObject1.materials;
        tailLightR = brakeLightObject2.materials;

        if (rearLight)
        {
            tailLightL[1] = brakeLightOn;
            tailLightR[1] = brakeLightOn;
        }
        else
        {
            tailLightL[1] = brakeLightOff;
            tailLightR[1] = brakeLightOff;

        }
        brakeLightObject1.materials = tailLightL;
        brakeLightObject2.materials = tailLightR;
        headLightL.intensity = headLightStatus;
        headLightR.intensity = headLightStatus;
    }

    private void removeCarParts(int index,int part) {
        if (carParts.Count > part)
        {
            carParts[index].transform.SetParent(null);
            carParts[index].AddComponent<BoxCollider>();
            carParts[index].AddComponent<Rigidbody>();
            carParts[index].GetComponent<Rigidbody>().angularVelocity = new Vector3(0,0,5f);
            carParts.RemoveAt(index);
        }
    }
    private void carHealthEffect(int carHealth)
    {
        int index = Random.Range(0, carParts.Count);
        switch (carHealth)
        {
            case 90:
                smoke.Play();
                return;

            case 80:
                darkSmoke.Play();
                smoke.Stop();
                return;

            case 70:
                smoke.Play();
                removeCarParts(index, 5);
                return;

            case 60:
                removeCarParts(index, 4);
                return;

            case 50:
                removeCarParts(index, 3);
                return;

            case 40:
                removeCarParts(index, 2);
                return;

            case 30:
                removeCarParts(index, 1);
                return;

            default:
                //Debug.Log("Do nothing yet");
                return;
        }
    }
}
