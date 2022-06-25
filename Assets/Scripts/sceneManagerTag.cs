using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sceneManagerTag : MonoBehaviour
{
    [SerializeField] carControllerOnWheels carControllerOnWheelsScript;
    [SerializeField] collisionTrigger collisionTriggerScript;
    [SerializeField] Image[] images;
    [SerializeField] Image backgroundImage;
    [SerializeField] TMP_Text displayGameTime;
    [SerializeField] TMP_Text servivalTimeText;
    private float timer = 0.0f;
    private int timeInSec;

    

    private void Update()
    {
        healthBar();
        if (carControllerOnWheelsScript.gameStatus())
        {
            timer += Time.deltaTime;
            timeInSec = (int)Mathf.Floor(timer);
            displayGameTime.text = timeInSec.ToString();
        }
        else
        {
            backgroundImage.GetComponent<Image>().enabled = false;
            displayGameTime.text = "";
        }
        
    }

    public void restertTime()
    {
        timer = 0.0f;
    }
    public int gameplayTime()
    {
        return timeInSec;
    }

    private void healthBar()
    {
        int currentCarHealth = collisionTriggerScript.getCarHealth();
        //Debug.Log(currentCarHealth);
        switch (currentCarHealth)
        {
            case int ex when currentCarHealth < 2:
                string timeInString = timeInSec.ToString();
                servivalTimeText.text = $"You Survived {timeInString} secs";
                images[7].GetComponent<Image>().enabled = false;
                return;

            case int ex when currentCarHealth < 12:
                images[6].GetComponent<Image>().enabled = false;
                return;

            case int ex when currentCarHealth < 25:
                images[5].GetComponent<Image>().enabled = false;
                return;

            case int ex when currentCarHealth < 37:
                images[4].GetComponent<Image>().enabled = false;
                return;

            case int ex when currentCarHealth < 50:
                images[3].GetComponent<Image>().enabled = false;
                return;

            case int ex when currentCarHealth < 62:
                images[2].GetComponent<Image>().enabled = false;
                return;

            case int ex when currentCarHealth < 75:
                images[1].GetComponent<Image>().enabled = false;
                return;

            case int ex when currentCarHealth < 87:
                images[0].GetComponent<Image>().enabled = false;
                return;
  
        }
    }
}
