using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance; 
    public TextMeshProUGUI scoreText; 
    /*
    private int playerScore = 0; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Scene de�i�ti�inde silinmesin
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        // Skor ekle ve g�ncelle
        playerScore += points;
        Debug.Log("Skor g�ncellendi: " + playerScore);

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // UI Text g�ncelle
        if (scoreText != null)
        {
            scoreText.text = "Score: " + playerScore;
        }
        else
        {
            Debug.LogWarning("ScoreText atanmad�!");
        }
    }*/
}
