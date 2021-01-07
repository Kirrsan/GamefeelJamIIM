using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public EnemyPack pack;
    public Text scoreNumber;

    private int currentScore = 0;

    public GameObject winPanel;
    public Button winButton;
    public GameObject losePanel;
    public Button loseButton;
    
    [Header("Score")]
    public AnimationCurve scaleDown;
    public float timeToScaleDown;
    private float timer;

    private Vector3 startScale;
    public Vector3 finalScale;
    private IEnumerator _scoreScaleCoroutine = null;

    // Start is called before the first frame update
    void Awake()
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
        scoreNumber.text = currentScore.ToString();
        if (_scoreScaleCoroutine != null)
        {
            StopCoroutine(_scoreScaleCoroutine);
        }
        _scoreScaleCoroutine = ScoreBounce();
        StartCoroutine(_scoreScaleCoroutine);
    }    

    private IEnumerator ScoreBounce()
    {
        startScale = scoreNumber.gameObject.transform.localScale;
        while (timer < timeToScaleDown)
        {
            timer += Time.deltaTime;
            float ratio = timer / timeToScaleDown;
            float curveRatio = scaleDown.Evaluate(ratio);
            scoreNumber.gameObject.transform.localScale = Vector3.Lerp(startScale, finalScale, curveRatio);
            yield return null;
        }

        timer = 0;
    }

    public void ResetScore()
    {
        scoreNumber.text = 0 + "";
    }

    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
