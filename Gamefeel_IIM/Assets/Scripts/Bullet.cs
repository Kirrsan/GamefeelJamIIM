using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speedBullet = 20f;
    [SerializeField] AnimationCurve bulletSpeedX;
    private float timerSpeedX = 0;
    [SerializeField] float direction = 1f;
    [SerializeField] float intensity = 1f;

    [SerializeField] GameObject bulletSprite;
    private float offsetToCenterX;
    private float offsetToCenterY;
    private float rotationSpeed;
    
    private bool _isPlayerFiring = false;

    private void Start()
    {
        if (!_isPlayerFiring) return;
        bulletSprite.transform.position = new Vector2(bulletSprite.transform.position.x + offsetToCenterX,
            bulletSprite.transform.position.y + offsetToCenterY);
    }

    public void SetParameters(float offsetX, float offsetY, float tempRotationSpeed)
    {
        offsetToCenterX = offsetX;
        offsetToCenterY = offsetY;
        rotationSpeed = tempRotationSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = transform.position;
        newPos.y += speedBullet * Time.deltaTime * direction;
        timerSpeedX += (Mathf.Sin(Time.time) + 1) * 0.5f;
        transform.position = newPos;

        if (!_isPlayerFiring) return;

        Vector2 newPosSprite = bulletSprite.transform.position;
        newPosSprite.x += bulletSpeedX.Evaluate(timerSpeedX) * Time.deltaTime * intensity;
        bulletSprite.transform.position = newPosSprite;
        
        Vector3 rotation = transform.eulerAngles;
        rotation.z += rotationSpeed * Time.deltaTime;
        transform.eulerAngles = rotation;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Collision !");
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        // collision.gameObject.GetComponent<PlayerCharacter>().DestroyPlayer;
    //        // GameManager.Instance.Lose();
    //        Debug.Log("Player Hit !");
    //        GameManager.Instance.loseGame();
    //    }
    //    /*
    //     * else if (collision.gameObject.CompareTag("Ennemy"))
    //    {
    //        collision.gameObject.GetComponent<Enemies>().DestroyEnemy();
    //        //add score
    //        Debug.Log("Ennemy Hit !");
    //    }
    //    */
    //    Destroy(this.gameObject);
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ennemy"))
    //    {
    //        collision.GetComponent<Enemies>().DestroyEnemy();
    //        GameManager.Instance.AddScore(10);
    //        Destroy(this.gameObject);
    //    }
    //}

    public void SetDirection(bool goingUp)
    {
        if (goingUp)
        {
            direction = 1f;
        }
        else direction = -1f;
    }
    
    public void SetObjectFiring(bool tempIsPlayerFiring)
    {
        _isPlayerFiring = tempIsPlayerFiring;
    }
}
