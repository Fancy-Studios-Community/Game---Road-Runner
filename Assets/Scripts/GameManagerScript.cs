using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public void gameOver()
    {
        Debug.Log("script access");
        Invoke("loadGame", 2f);
        Time.timeScale = 0f;
        
    }

    void loadGame()
    {
        SceneManager.LoadScene(2);

    }
}
