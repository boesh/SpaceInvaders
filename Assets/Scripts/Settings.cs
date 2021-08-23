using UnityEngine;
using TMPro;

public class Settings : Singleton<Settings>
{
    [SerializeField, Range(4,7)] public float borders;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    private int score;

    private void Awake()
    {
        borders = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }

    public void Win()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "YOU WIN!";
        Time.timeScale = 0f;

    }
    public void Lose()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "YOU LOSE:(";
        Time.timeScale = 0f;
    }

}