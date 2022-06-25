using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private static bool GamePause = false;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject touchInputCanvus;
    [SerializeField] MainMenu mainMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePause)
            {
                resume();
            }
            else
            {
                pause();
            }
        
            
        }
    }

    

    private void pause()
    {
        pauseMenuUI.SetActive(true);
        touchInputCanvus.SetActive(false);
        Time.timeScale = 0f;
        GamePause = true;
    }

    public void resume()
    {
        touchInputCanvus.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePause = false;
    }

    public void startMeunuRedirect()
    {
        Time.timeScale = 1f;
        GamePause = false;
        mainMenu.toStartMenu();
    }
}
