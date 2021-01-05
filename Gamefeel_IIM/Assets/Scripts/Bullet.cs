using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speedBullet = 20f;
    [SerializeField] float direction = 1f;



    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = transform.position;
        newPos.y += speedBullet * Time.deltaTime * direction;
        transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision !");
        if (collision.gameObject.CompareTag("Player"))
        {
            // collision.gameObject.GetComponent<PlayerCharacter>().DestroyPlayer;
            // GameManager.Instance.Lose();
            Debug.Log("Player Hit !");
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
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemy"))
        {
            collision.GetComponent<Enemies>().DestroyEnemy();
            GameManager.Instance.AddScore(10);
            Destroy(this.gameObject);
        }
    }

    public void SetDirection(bool goingUp)
    {
        if (goingUp)
        {
            direction = 1f;
        }
        else direction = -1f;
    }
}
