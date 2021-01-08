using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletPositionOffset;
    [SerializeField] Animator enemyAnim;

    private int columnNumber = 0;
    private int rowNumber = 0;


    private bool hasShield = true;
    public GameObject shield;
    private SpriteRenderer shieldSprite;
    [SerializeField] float shieldExplositionDuration = 0.5f;
    [SerializeField] float shieldExplosionRadius = 4f;


    public GameObject ennemyResidual;
    private Vector2 startPos;
    float residualDuration = 0.3f;


    public AudioSource shieldSound;
    public AudioSource ennemyImpact;
    public AudioSource ennemyShoot;
    public AudioSource ennemyFallSound;


    private void Start()
    {
        shieldSprite = shield.GetComponent<SpriteRenderer>();

        startPos = transform.position;
    }


    public void SetIds(int tempColumnNumber, int tempRowNumber)
    {
        columnNumber = tempColumnNumber;
        rowNumber = tempRowNumber;
    }
    
    public void ReduceColumnNumber()
    {
        columnNumber -= 1;
    }

    public void ChangeDirection(int direction)
    {
        enemyAnim.SetInteger("Direction", direction);
    }

    public void StartShoot()
    {
        if (FeedbackController.Instance.hasEnemyShootEffect)
        {
            enemyAnim.SetTrigger("Shoot");
            ennemyShoot.Play();
        }
        else
        {
            Shoot();
        }
    }
    
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bullet.transform.position = new Vector2(transform.position.x, transform.position.y - bulletPositionOffset);
        bulletScript.SetDirection(false);
        bulletScript.SetObjectFiring(false);
    }

    public void DestroyEnemy()
    {
        if (hasShield)
        {
            if (FeedbackController.Instance.hasEnemyMovementParticle)
            {
                shieldSound.Play();
            }
            StartCoroutine(DestroyShield());
        }
        else
        {
            Debug.Log("Enemy destroyed");
            GameManager.Instance.pack.RemoveColumn(columnNumber, rowNumber);
            GetComponent<BoxCollider2D>().enabled = false;
            enemyAnim.SetTrigger("Dead");
            if (FeedbackController.Instance.hasEnemyMovementParticle)
            {
                ennemyFallSound.Play();
                ennemyImpact.PlayDelayed(1f);
            }
        }
        // Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            GameManager.Instance.pack.MoveDown();
            //Debug.Log("walltouched");
        }
        else if (other.CompareTag("Player"))
        {
            GameManager.Instance.loseGame();
        }        
        else if (other.CompareTag("Lose"))
        {
            GameManager.Instance.loseGame();
        }
    }

    IEnumerator DestroyShield()
    {
        float timer = 0f;
        float alpha = 1f;
        while (timer < shieldExplositionDuration)
        {
            timer += Time.deltaTime;
            shield.transform.localScale = Vector3.Lerp(Vector3.one, shieldExplosionRadius * Vector3.one, timer / shieldExplositionDuration);
            alpha = Mathf.Lerp(1f, 0f, timer / shieldExplositionDuration);
            shieldSprite.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }
        hasShield = false;
    }

    public bool GetHasShield()
    {
        return hasShield;
    }

    public IEnumerator EnnemyResidual(Vector2 newPos)
    {
        float tim = 0f;
        while (tim < residualDuration)
        {
            tim += Time.deltaTime;
            ennemyResidual.transform.position = Vector2.Lerp(startPos, newPos, tim / residualDuration);
            yield return null;
        }
        startPos = newPos;
    }
}
