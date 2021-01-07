using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] float bulletPositionOffset;
    [SerializeField] Animator enemyAnim;

    private int columnNumber = 0;
    private int rowNumber = 0;

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
    
    public void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = new Vector2(transform.position.x, transform.position.y - bulletPositionOffset);
        bullet.SetDirection(false);
        bullet.SetObjectFiring(false);
        enemyAnim.SetTrigger("Shoot");
    }

    public void DestroyEnemy()
    {
        Debug.Log("Enemy destroyed");
        GameManager.Instance.pack.RemoveColumn(columnNumber, rowNumber);
        enemyAnim.SetTrigger("Dead");

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
    }
}
