using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;

    public void AddScore()
    {
        score++;
        scoreText.text = "score:" + score;
    }
}
