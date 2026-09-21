using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseWin : MonoBehaviour
{
    
    public GameObject winUI;
    public GameObject gameOverUI;

    public Transform playerCamera;


    public string mainMenuSceneName = "MainMenu"; 

    void Start()
    {
        if (winUI != null) winUI.SetActive(false);
        if (gameOverUI != null) gameOverUI.SetActive(false);

        Time.timeScale = 1;
        
    }



    // Anropa den här när spelaren VINNER (t.ex. från ditt poängskript)
    public void TriggerWin()
    {
        if (winUI != null)
        {
            PositionMenuInFrontOfPlayer(winUI);
            winUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // Anropa den här när spelaren FÖRLORAR (t.ex. från ditt poängskript)
    public void TriggerGameOver()
    {
        if (gameOverUI != null)
        {
            PositionMenuInFrontOfPlayer(gameOverUI);
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
        }
    }


    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void PositionMenuInFrontOfPlayer(GameObject menu)
    {
        if (playerCamera != null)
        {
            menu.transform.position = playerCamera.position + playerCamera.forward * 2f;

            Vector3 lookDir = playerCamera.forward;
            lookDir.y = 0; 
            menu.transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }
}