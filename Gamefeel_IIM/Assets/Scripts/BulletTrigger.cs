using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{

    public GameObject ScoreToDisplay;
    public GameObject hitShield;

    public AudioSource playerImpactSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemy"))
        {
            Enemies enemyScript = collision.GetComponent<Enemies>();
            enemyScript.DestroyEnemy();
            if (FeedbackController.Instance.hasPlayerShootEffect && enemyScript.GetHasShield())
            {
                GameObject shieldBreakEffect = Instantiate(hitShield, transform.position, Quaternion.identity);
                Destroy(shieldBreakEffect, 1);
            }
            GameManager.Instance.AddScore(10);
            if (FeedbackController.Instance.hasUIEffect)
            {
                GameObject scoreText = Instantiate(ScoreToDisplay, collision.transform.position, Quaternion.identity);
            }
            Destroy(this.transform.parent.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision !");
        if (collision.gameObject.CompareTag("Player"))
        {
            // collision.gameObject.GetComponent<PlayerCharacter>().DestroyPlayer;
            // GameManager.Instance.Lose();
            Debug.Log("Player Hit !");

            if (FeedbackController.Instance.hasPlayerMovementParticle)
            {
                playerImpactSound.Play();
            }
            GameManager.Instance.loseGame();
        }
        /*
         * else if (collision.gameObject.CompareTag("Ennemy"))
        {
            collision.gameObject.GetComponent<Enemies>().DestroyEnemy();
            //add score
            Debug.Log("Ennemy Hit !");
        }
        */
        Destroy(this.transform.parent.gameObject);
    }
}
