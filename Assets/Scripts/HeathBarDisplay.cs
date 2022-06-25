using UnityEngine;
using UnityEngine.UI;

public class HeathBarDisplay : MonoBehaviour
{
    [SerializeField] Image[] heathBarImage;
    [SerializeField] collisionTrigger collisionTriggerScript;

    private void Update()
    {
        int carHeath = collisionTriggerScript.getCarHealth();
        displayHeath(carHeath);
    }

    private void displayHeath(int health)
    {
        switch (health)
        {
            case int ex when health < 5:
                heathBarImage[9].GetComponent<Image>().enabled = false;
                return;

            case int ex when health < 10:
                heathBarImage[8].GetComponent<Image>().enabled = false;
                return;

            case int ex when health < 20:
                heathBarImage[7].GetComponent<Image>().enabled = false;
                return;

            case int ex when health < 30:
                heathBarImage[6].GetComponent<Image>().enabled = false;
                return;

            case int ex when health < 40:
                heathBarImage[5].GetComponent<Image>().enabled = false;
                return;

            case int ex when health < 50:
                heathBarImage[4].GetComponent<Image>().enabled = false;
                return;

            case int ex when health < 60:
                heathBarImage[3].GetComponent<Image>().enabled = false;
                return;

            case int ex when health < 70:
                heathBarImage[2].GetComponent<Image>().enabled = false;
                return;

            case int ex when health < 80:
                heathBarImage[1].GetComponent<Image>().enabled = false;
                return;

            case int ex when health < 90:
                heathBarImage[0].GetComponent<Image>().enabled = false;
                return;

           
        }
    }

}
