using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{

    public TMP_Text playerScoreText;
    public TMP_Text opponentScoreText;

    public LoseWin player1UI;
    public LoseWin player2UI;

    public int playerScore = 0;
    public int opponentScore = 0;
    public int maxScore = 11;

    private bool isGameOver = false;

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddPlayerScore()
    {
        if (isGameOver) return;

        playerScore++;
        UpdateScoreUI();
        CheckWinCondition();
    }

    public void AddOpponentScore()
    {
        if (isGameOver) return;

        opponentScore++;
        UpdateScoreUI();
        CheckWinCondition();
    }

    private void UpdateScoreUI()
    {
        if (playerScoreText != null)
            playerScoreText.text = playerScore.ToString();

        if (opponentScoreText != null)
            opponentScoreText.text = opponentScore.ToString();
    }

    private void CheckWinCondition()
    {
        if (playerScore >= maxScore)
        {
            isGameOver = true;
            player1UI.TriggerWin(playerScore, opponentScore);
            player2UI.TriggerGameOver(opponentScore, playerScore);
        }
        else if (opponentScore >= maxScore)
        {
            isGameOver = true;
            player1UI.TriggerGameOver(playerScore, opponentScore);
            player2UI.TriggerWin(opponentScore, playerScore);
        }
    }


}
