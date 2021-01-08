using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FeedbackController : MonoBehaviour
{
    public static FeedbackController Instance;
    private bool isPaused = false;
    public GameObject pauseCanvas;
    private Button returnButton;

    public UnityEvent onPlayerMovementDeactivationEvent;
    public UnityEvent onPlayerMovementActivationEvent;
    public UnityEvent onUIDeactivationEvent;
    
    public bool hasPlayerMovementParticle = true;
    private Text PlayerMovementParticle;
    
    public bool hasPlayerShootEffect = true;
    private Text PlayerShootEffect;
    
    public bool hasEnemyMovementParticle = true;
    private Text EnemyMovementParticle;
    
    public bool hasEnemyShootEffect = true;
    private Text EnemyShootEffect;
    
    public bool hasBackgroundEffect = true;
    private Text BackgroundEffect;
    
    public bool hasUIEffect = true;
    private Text UIEffect;

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

        PlayerMovementParticle = pauseCanvas.transform.Find("Panel/PlayerMovementParticles").GetComponent<Text>();
        PlayerShootEffect = pauseCanvas.transform.Find("Panel/PlayerShootEffect").GetComponent<Text>();
        EnemyMovementParticle = pauseCanvas.transform.Find("Panel/EnemyMovementParticle").GetComponent<Text>();
        EnemyShootEffect = pauseCanvas.transform.Find("Panel/EnemyShootEffect").GetComponent<Text>();
        BackgroundEffect = pauseCanvas.transform.Find("Panel/BackgroundEffect").GetComponent<Text>();
        UIEffect = pauseCanvas.transform.Find("Panel/UIEffect").GetComponent<Text>();


        UpdateText(PlayerMovementParticle, "PlayerMovementParticle (A)", hasPlayerMovementParticle);
        UpdateText(PlayerShootEffect, "PlayerShootEffect (Z)", hasPlayerShootEffect);
        UpdateText(EnemyMovementParticle, "EnemyMovementParticle (E)", hasEnemyMovementParticle);
        UpdateText(EnemyShootEffect, "EnemyShootEffect (R)", hasEnemyShootEffect);
        UpdateText(BackgroundEffect, "BackgroundEffect (T)", hasBackgroundEffect);
        UpdateText(UIEffect, "UIEffect (Y)", hasUIEffect);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PausePlay();

        if (Input.GetKeyDown(KeyCode.A))
        {
            hasPlayerMovementParticle = !hasPlayerMovementParticle;
            if (!hasPlayerMovementParticle)
            {
                onPlayerMovementDeactivationEvent.Invoke();
            }
            else
            {
                onPlayerMovementActivationEvent.Invoke();
            }
            UpdateText(PlayerMovementParticle, "PlayerMovementParticle (A)", hasPlayerMovementParticle);
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            hasPlayerShootEffect = !hasPlayerShootEffect;
            UpdateText(PlayerShootEffect, "PlayerShootEffect (Z)", hasPlayerShootEffect);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            hasEnemyMovementParticle = !hasEnemyMovementParticle;
            UpdateText(EnemyMovementParticle, "EnemyMovementParticle (E)", hasEnemyMovementParticle);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            hasEnemyShootEffect = !hasEnemyShootEffect;
            UpdateText(EnemyShootEffect, "EnemyShootEffect (R)", hasEnemyShootEffect);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            hasBackgroundEffect = !hasBackgroundEffect;
            UpdateText(BackgroundEffect, "BackgroundEffect (T)", hasBackgroundEffect);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            hasUIEffect = !hasUIEffect;
            if (!hasUIEffect)
            {
                onUIDeactivationEvent.Invoke();
            }
            UpdateText(UIEffect, "UIEffect (Y)", hasUIEffect);
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
