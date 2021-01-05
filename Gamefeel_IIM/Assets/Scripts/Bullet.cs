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
            Debug.Log("Player Hit !");
        }
        else if (collision.gameObject.CompareTag("Ennemy"))
        {
            Debug.Log("Ennemy Hit !");
        }
        Destroy(this);
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
