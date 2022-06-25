using UnityEngine;

public class manageScene3b : MonoBehaviour
{
    [SerializeField] carControllerOnWheels carControllerOnWheelsScript;
    [SerializeField] GameObject restartCanvus;
    [SerializeField] GameObject scoreHeathSpeed;


    private void restartMenuSpawn(bool result)
    {
        if (result == false) 
        {
            scoreHeathSpeed.SetActive(false);
            restartCanvus.SetActive(true);
        }
    }

    private void Update()
    {
        bool result = carControllerOnWheelsScript.gameStatus();
        restartMenuSpawn(result);
        //Debug.Log(tilt);
    }
}
