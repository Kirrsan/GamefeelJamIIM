using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public EnemyPack pack;
    public Text score;

    private int currentScore = 0;

    public GameObject winPanel;
    public Button winButton;
    public GameObject losePanel;
    public Button loseButton;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        winButton.onClick.AddListener(() => resetGame());
        loseButton.onClick.AddListener(() => resetGame());
    }

    public void winGame()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

    public void loseGame()
    {
        Time.timeScale = 0f;
        losePanel.SetActive(true);
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        score.text = currentScore.ToString();
    }

    public void ResetScore()
    {
        score.text = 0 + "";
    }

    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
