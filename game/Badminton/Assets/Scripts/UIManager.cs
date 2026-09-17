using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Main Menu Paneler")]
    public GameObject mainPanel;
    public GameObject rulesPanel;

    [Header("Spel-UI & Tavlor")]
    public GameObject mainMenuCanvas;
    public GameObject scoreBoardCanvas;
    public GameObject gameOver;

    [Header("Textfält")]
    public TMP_Text scoretext;
    public TMP_Text resultText;

    private int currentScore = 0;
    private string selectedDifficulty = "Normal"; // Sparar vald svårighetsgrad

    void Start()
    {
        // Visar bara huvudmenyn när spelet startar
        if (mainPanel != null) mainPanel.SetActive(true);
        if (rulesPanel != null) rulesPanel.SetActive(false);

        if (mainMenuCanvas != null) mainMenuCanvas.SetActive(true);
        if (scoreBoardCanvas != null) scoreBoardCanvas.SetActive(false);
        if (gameOver != null) gameOver.SetActive(false);
    }

    // --- Meny-navigering ---
    public void ShowRules()
    {
        if (mainPanel != null) mainPanel.SetActive(false);
        if (rulesPanel != null) rulesPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        if (rulesPanel != null) rulesPanel.SetActive(false);
        if (mainPanel != null) mainPanel.SetActive(true);
    }

    // --- Välj Svårighetsgrad ---
    public void SetEasyMode()
    {
        selectedDifficulty = "Easy";
        Debug.Log("Svårighetsgrad vald: Easy");
    }

    public void SetNormalMode()
    {
        selectedDifficulty = "Normal";
        Debug.Log("Svårighetsgrad vald: Normal");
    }

    public void SetHardMode()
    {
        selectedDifficulty = "Hard";
        Debug.Log("Svårighetsgrad vald: Hard");
    }

    // --- Spel-flöde ---
    public void StartGame()
    {
        // Stänger av hela startmenyn och tänds poängtavlan
        if (mainMenuCanvas != null) mainMenuCanvas.SetActive(false);
        if (scoreBoardCanvas != null) scoreBoardCanvas.SetActive(true);
        if (gameOver != null) gameOver.SetActive(false);

        currentScore = 0;
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoretext != null)
            scoretext.text = "Poäng: " + currentScore;
    }

    public void TriggerGameOver(bool playerWon)
    {
        if (gameOver != null) gameOver.SetActive(true);

        if (resultText != null)
        {
            resultText.text = playerWon ? "VINST!\nPoäng: " + currentScore : "FÖRLUST!\nPoäng: " + currentScore;
        }
    }
}