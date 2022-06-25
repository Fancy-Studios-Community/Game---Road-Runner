using UnityEngine;
using TMPro;
public class ScoreAndOtherThings : MonoBehaviour
{

    [SerializeField] Transform carPosition;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI scoreOnRestartScreen;
    private Vector3 startCarPosition;
    private float totalDistance = 0f;
    [SerializeField] Transform terrainTransform;
    [SerializeField] bool thisScriptOnEnemyCar;

    private void Start()
    {
        startCarPosition = terrainTransform.InverseTransformPoint(carPosition.position);
    }
    private void Update()
    {
        Vector3 carPositionRelative = terrainTransform.InverseTransformPoint(carPosition.position);
        float distanceTravelledThisFrame = Vector3.Distance(carPositionRelative,startCarPosition); 
        Vector3 directionOfMovement = (carPositionRelative - startCarPosition).normalized; 
       
        float dot = Vector3.Dot(carPosition.forward, directionOfMovement);
        if (dot > 0f)
        {
            totalDistance += distanceTravelledThisFrame;
            startCarPosition = carPositionRelative;
            if(!thisScriptOnEnemyCar)
            {
                score.SetText(totalDistance.ToString("0"));
                scoreOnRestartScreen.SetText(totalDistance.ToString("0"));

            }
        }
    }

    public float getTotalDistance() //called by enemyCarControllerOnWheels
    {
        return totalDistance;
    }
}
