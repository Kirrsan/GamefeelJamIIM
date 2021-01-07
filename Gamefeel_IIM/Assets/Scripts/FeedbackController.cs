using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackController : MonoBehaviour
{
    public static FeedbackController Instance;
    private bool isPaused = false;
    public GameObject pauseCanvas;
    private Button returnButton;

    public bool hasSparksEffect = true;
    private Text Sparks;

    public bool hasSmokeEffect = true;
    private Text Smoke;

    public bool hasBulletRotating = true;
    private Text ShootRotating;

    public bool hasBulletTrail = true;
    private Text BulletTrail;

    public bool hasRecoilEffect = true;
    private Text Recoil;

    public bool hasScoreEffect = true;
    private Text Score;

    public bool hasPlayerVibrating = true;
    private Text Vibrating;

    public bool hasShootParticle = true;
    private Text ShootParticle;
    
    public bool hasPlayerMovementParticle = true;
    private Text PlayerMovementParticle;

    public bool hasCrackEffect = true;
    private Text Crack;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        returnButton = pauseCanvas.transform.Find("ReturnButton").GetComponent<Button>();
        returnButton.onClick.AddListener(() => PausePlay());

        Sparks = pauseCanvas.transform.Find("Panel/Sparks").GetComponent<Text>();
        Smoke = pauseCanvas.transform.Find("Panel/Smoke").GetComponent<Text>();
        ShootRotating = pauseCanvas.transform.Find("Panel/ShootRotating").GetComponent<Text>();
        BulletTrail = pauseCanvas.transform.Find("Panel/BulletTrail").GetComponent<Text>();
        Recoil = pauseCanvas.transform.Find("Panel/Recoil").GetComponent<Text>();
        Score = pauseCanvas.transform.Find("Panel/Score").GetComponent<Text>();
        Vibrating = pauseCanvas.transform.Find("Panel/Vibrating").GetComponent<Text>();
        ShootParticle = pauseCanvas.transform.Find("Panel/ShootParticle").GetComponent<Text>();
        Crack = pauseCanvas.transform.Find("Panel/Crack").GetComponent<Text>();

        UpdateText(Sparks, "Sparks (A)", hasSparksEffect);
        UpdateText(Smoke, "Smoke (Z)", hasSmokeEffect);
        UpdateText(ShootRotating, "ShootRotating (E)", hasBulletRotating);
        UpdateText(BulletTrail, "BulletTrail (R)", hasBulletTrail);
        UpdateText(Recoil, "Recoil on Shoot (T)", hasRecoilEffect);
        UpdateText(Score, "Score (Y)", hasScoreEffect);
        UpdateText(Vibrating, "Vibrating on Start (U)", hasPlayerVibrating);
        UpdateText(ShootParticle, "ShootParticle (I)", hasShootParticle);
        UpdateText(Crack, "Crack (O)", hasCrackEffect);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PausePlay();

        if (Input.GetKeyDown(KeyCode.A))
        {
            hasSparksEffect = !hasSparksEffect;
            UpdateText(Sparks, "Sparks (A)", hasSparksEffect);
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            hasSmokeEffect = !hasSmokeEffect;
            UpdateText(Smoke, "Smoke (Z)", hasSmokeEffect);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            hasBulletRotating = !hasBulletRotating;
            UpdateText(ShootRotating, "ShootRotating (E)", hasBulletRotating);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            hasBulletTrail = !hasBulletTrail;
            UpdateText(BulletTrail, "BulletTrail (R)", hasBulletTrail);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            hasRecoilEffect = !hasRecoilEffect;
            UpdateText(Recoil, "Recoil on Shoot (T)", hasRecoilEffect);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            hasScoreEffect = !hasScoreEffect;
            UpdateText(Score, "Score (Y)", hasScoreEffect);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            hasPlayerVibrating = !hasPlayerVibrating;
            UpdateText(Vibrating, "Vibrating on Start (U)", hasPlayerVibrating);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            hasShootParticle = !hasShootParticle;
            UpdateText(ShootParticle, "ShootParticle (I)", hasShootParticle);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            hasCrackEffect = !hasCrackEffect;
            UpdateText(Crack, "Crack (O)", hasCrackEffect);
        }
    }

    private void PausePlay()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0f;
            pauseCanvas.SetActive(true);
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
            pauseCanvas.SetActive(false);
        }
    }

    private void UpdateText(Text textObject, string beforeValue, bool newValue)
    {
        textObject.text = beforeValue + " : " + newValue.ToString();
    }
}
