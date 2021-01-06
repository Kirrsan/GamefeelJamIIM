using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Bullet bulletPrefab;
    [SerializeField] float speed = 1f;
    [SerializeField] float maxXPosition = 8f;
    [SerializeField] float timeBetweenShoot = 1f;

    private float shootTimer = 0f;
    [SerializeField] float timeForLoadShoot = 3f;

    [SerializeField] float bulletPositionOffset = 1f;
    
    [Header("Bullet Rotation")]
    [SerializeField] float spriteOffsetToCenterX;
    [SerializeField] float spriteOffsetToCenterY;
    [SerializeField] float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
    }

    public void Move(bool goRight)
    {
        Vector2 newPos = transform.position;
        if (goRight)
        {
            newPos.x += speed * Time.deltaTime;
            newPos.x = Mathf.Min(newPos.x, maxXPosition);
        }
        else
        {
            newPos.x -= speed * Time.deltaTime;
            newPos.x = Mathf.Max(newPos.x, -maxXPosition);
        }

        transform.position = newPos;
    }

    public void Shoot(float loadShootTimer)
    {
        if (shootTimer < timeBetweenShoot) return;

        shootTimer = 0f;

        if (loadShootTimer < timeForLoadShoot)
        {
            Debug.Log("Small Shoot !");
        }
        else
        {
            Debug.Log("Big SHOOT !!");
        }
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = new Vector2(transform.position.x, transform.position.y + bulletPositionOffset);
        bullet.SetDirection(true);
        bullet.SetObjectFiring(true);
        bullet.SetParameters(spriteOffsetToCenterX, spriteOffsetToCenterY, rotationSpeed);
    }
}
