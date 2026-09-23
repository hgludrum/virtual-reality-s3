using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LoseWin : MonoBehaviour
{
    
    public GameObject winUI;
    public GameObject gameOverUI;
    public Transform playerCamera;


    public TMP_Text winPlayerScoreText;
    public TMP_Text winOpponentScoreText;


    public TMP_Text losePlayerScoreText;
    public TMP_Text loseOpponentScoreText;


    public string mainMenuSceneName = "MainMenu";

    [SerializeField] private AudioClip victoryClip;
    [SerializeField] private AudioClip defeatClip;
    [SerializeField] private AudioSource audioSource;


    void Start()
    {
        if (winUI != null) winUI.SetActive(false);
        if (gameOverUI != null) gameOverUI.SetActive(false);

        Time.timeScale = 1;
        
    }

   
    // Anropa den här när spelaren VINNER (t.ex. från ditt poängskript)
    public void TriggerWin(int myScore, int opponentScore)
    {
        if (winPlayerScoreText != null)
            winPlayerScoreText.text = myScore.ToString();

        if (winOpponentScoreText != null)
            winOpponentScoreText.text = opponentScore.ToString();


        if (victoryClip != null && audioSource != null)
        {
            audioSource.ignoreListenerPause = true;
            audioSource.PlayOneShot(victoryClip, 0.9f);
        }


        if (winUI != null)
        {
            PositionMenuInFrontOfPlayer(winUI);
            winUI.SetActive(true);
            Time.timeScale = 0;

        }
    }

    // Anropa den här när spelaren FÖRLORAR (t.ex. från ditt poängskript)
    public void TriggerGameOver(int myScore, int opponentScore)
    {
        if (losePlayerScoreText != null)
            losePlayerScoreText.text = myScore.ToString();

        if (loseOpponentScoreText != null)
            loseOpponentScoreText.text = opponentScore.ToString();

        if (defeatClip != null && audioSource != null)
        {
            audioSource.ignoreListenerPause = true;
            audioSource.PlayOneShot(defeatClip, 0.9f);
        }

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