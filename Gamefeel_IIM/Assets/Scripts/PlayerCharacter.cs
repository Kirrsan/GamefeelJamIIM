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

    [SerializeField] AnimationCurve shakeIntensityCurve;
    [SerializeField] float maxShakeIntensity = 0.05f;
    [SerializeField] float shakeDuration = 1f;
    private Vector2 startPos;

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
    }


    private void Start()
    {
        startPos = transform.position;
        StartCoroutine(ShakePlayer());
        // PlaySound
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
    }

    IEnumerator ShakePlayer()
    {
        float timer = 0f;
        float shakeIntensity = 0f;
        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;
            shakeIntensity = maxShakeIntensity * shakeIntensityCurve.Evaluate(timer / shakeDuration);
            //transform.position = startPos + shakeIntensity * new Vector2(
            //    Mathf.PerlinNoise(speed * Time.time, 1),
            //    Mathf.PerlinNoise(speed * Time.time, 2));

            transform.position = startPos + UnityEngine.Random.insideUnitCircle * shakeIntensity;
            yield return null;
        }

        transform.position = startPos;
    }
}
