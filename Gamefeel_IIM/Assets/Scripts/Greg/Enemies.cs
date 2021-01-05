using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] float bulletPositionOffset;

    private int columnNumber = 0;

    public void SetIds(int tempColumnNumber)
    {
        columnNumber = tempColumnNumber;
    }
    
    public void ReduceColumnNumber()
    {
        columnNumber -= 1;
    }
    
    public void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = new Vector2(transform.position.x, transform.position.y - bulletPositionOffset);
        bullet.SetDirection(false);
    }

    public void DestroyEnemy()
    {
        Debug.Log("Enemy destroyed");
        GameManager.Instance.pack.RemoveColumn(columnNumber);
        
        Destroy(this.gameObject);
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
    }
}
