using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    
    [SerializeField] int loadSceneNumber;
    
    
    //private bool loginDisplayStatus = false;


    public void PlayGame()
    {
        
        switch (loadSceneNumber)
        {
            case 0:
                SceneManager.LoadScene(1);
                //if (false) StartCoroutine(fadeText());
                //else SceneManager.LoadScene(1);
                return;

            case 1:
                SceneManager.LoadScene(1);
                return;

            case 2:
                SceneManager.LoadScene(2);
                return;

            default:
                //do nothing
                return;
        }
    
    }

    
   
  

    public void QuitGame()
    {
        Application.Quit();
        
    }
    /*
    private void loginEnableDisable()
    {
        PlayerData playerData = SaveSystem.loadPlayer();
        if (playerData == null) return;
        //user exist
        string user = playerData.user;
        username.text = $"Logged in as {user}";
        LogInButton.SetActive(false);
        loginDisplayStatus = true;
        
    }
    
    private void Update()
    {
        if(!loginDisplayStatus && loadSceneNumber == 0) loginEnableDisable();
    }
    */

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void derby()
    {
        SceneManager.LoadScene(2);
    }

    public void roadRunner()
    {
        SceneManager.LoadScene(1);
    }

    public void toStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
