using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TMP_Text highScoreText;

    void Start()
    {
        DisplayHighScore();
    }

    public void DisplayHighScore()
    {
        // Retrieve the high score from PlayerPrefs
        int highScore = PlayerPrefs.GetInt("highscore", 0);

        // Update the TextMeshPro text element to display the high score
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt("highscore", score);
        PlayerPrefs.Save();
    }
}
