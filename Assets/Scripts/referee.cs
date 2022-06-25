using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class referee : MonoBehaviour
{
    [SerializeField] GameObject[] cars;
    private Vector3[] oldPosition;
    private float[] timeSinceLastMovement;
    private Vector3[] resetPosition;
    [SerializeField] carControllerOnWheels carControllerOnWheelsScript;
    [SerializeField] GameObject gameOverCanves;

    private void Awake()
    {
        oldPosition = new Vector3[cars.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            oldPosition[i] = cars[i].transform.position;
        }

        timeSinceLastMovement = new float[cars.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            timeSinceLastMovement[i] = Time.realtimeSinceStartup;
        }
        resetPosition = new Vector3[cars.Length];
        oldPosition.CopyTo(resetPosition, 0);
    }

    private void checkMovement()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            if (Vector3.Distance(cars[i].transform.position, oldPosition[i]) < 2)
            {
                //car barely mooving
                //reset car position if stuck for more than 2 sec
                if (Time.realtimeSinceStartup - timeSinceLastMovement[i] > 4)
                {
                    cars[i].transform.position = resetPosition[i];
                    if (i == 0) cars[i].transform.rotation = Quaternion.Euler(Vector3.up);
                    //Debug.Log(Time.realtimeSinceStartup - timeSinceLastMovement[i]);
                    //Debug.Log(resetPosition[i]);
                }
            }

            else
            {
                oldPosition[i] = cars[i].transform.position;
                timeSinceLastMovement[i] = Time.realtimeSinceStartup;
            }
        }
    }

    
    private void Update()
    {
        if (carControllerOnWheelsScript.gameStatus()) checkMovement();
        else
        {
            gameOverCanves.SetActive(true);
            
        }
    }
}
