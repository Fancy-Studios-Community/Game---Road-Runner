using UnityEngine;
using Cinemachine;

public class collisionTrigger : MonoBehaviour
{
    [SerializeField] bool crashImpulseWanted = true;
    private carControllerOnWheels carControllerOnWheelsScript;
    [SerializeField] CinemachineImpulseSource cinemachineImpulseSource;

    private Vector3 oldCarPosition = Vector3.zero; 
    private int carHealth = 99;

    private void Awake()
    {
        carControllerOnWheelsScript = GetComponent<carControllerOnWheels>();
    }
    private void shakeCamera()
    {
        if(carControllerOnWheelsScript.gameStatus()) cinemachineImpulseSource.GenerateImpulse();

    }

    private void OnCollisionEnter(Collision other)
    {
        float totalDistance = Vector3.Distance(transform.position, oldCarPosition);
        if (totalDistance > 2f)
        {
            carHealth--;
            if(crashImpulseWanted) shakeCamera();
            oldCarPosition = transform.position;
        }
        if (other.gameObject.tag == "enemy") carHealth--;
    }
    public int getCarHealth() //called by healthBarDisplay script, carControllerOnWheels & CarSpecialEffects script
    {
        return carHealth;
    }

       
}
