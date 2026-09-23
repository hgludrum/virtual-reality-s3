using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public enum Difficulty { Easy, Normal, Hard }


    public TMP_Text buttonText; 


    public static Difficulty currentDifficulty = Difficulty.Easy;

    void Start()
    {
        UpdateButtonUI();
        Time.timeScale = 0;
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CycleDifficulty()
    {
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                currentDifficulty = Difficulty.Normal;
                break;
            case Difficulty.Normal:
                currentDifficulty = Difficulty.Hard;
                break;
            case Difficulty.Hard:
                currentDifficulty = Difficulty.Easy;
                break;
        }

        UpdateButtonUI();
    }

    private void UpdateButtonUI()
    {
        if (buttonText != null)
        {
            buttonText.text = currentDifficulty.ToString().ToUpper();
        }
    }
}

