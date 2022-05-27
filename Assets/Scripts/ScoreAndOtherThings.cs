using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreAndOtherThings : MonoBehaviour
{

    public Transform carPosition;
    public TextMeshProUGUI score;
    public TextMeshProUGUI scoreOnRestartScreen;
    private Vector3 startCarPosition;
    public float totalDistance = 0f;

    private void Start()
    {
        startCarPosition = carPosition.position;
    }
    private void Update()
    {
        float distanceTravelledThisFrame = (carPosition.position - startCarPosition).magnitude;
        totalDistance += distanceTravelledThisFrame;
        startCarPosition = carPosition.position;
        score.SetText(totalDistance.ToString("0"));
        scoreOnRestartScreen.SetText(totalDistance.ToString("0"));
        //Debug.Log(totalDistance.ToString("0"));
    }
}
