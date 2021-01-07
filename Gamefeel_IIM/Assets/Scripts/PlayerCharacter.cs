using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] float speed = 1f;
    [SerializeField] float maxXPosition = 8f;
    [SerializeField] float timeBetweenShoot = 1f;


    private bool canMove = true;
    private bool canShoot = false;
    private float shootTimer = 0f;
    private float shootHeldTimer = 0f;
    [SerializeField] float timeForLoadShoot = 3f;

    [SerializeField] float bulletPositionOffset = 1f;
    
    [Header("Bullet Rotation")]
    [SerializeField] float spriteOffsetToCenterX;
    [SerializeField] float spriteOffsetToCenterY;
    [SerializeField] float rotationSpeed;

    [SerializeField] AnimationCurve shakeIntensityCurve;
    [SerializeField] float maxShakeIntensity = 0.05f;
    [SerializeField] float shakeDuration = 1f;
    private Vector2 startPos;

    public GameObject playerSprite;
    [SerializeField] float recoilDuration = 1f;
    [SerializeField] float maxRecoil = 0.2f;
    [SerializeField] AnimationCurve recoilCurve;

    public ParticleSystem bulletShootParticles;

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
    }

    public IEnumerator SwitchMode(Animator anim)
    {
        anim.SetTrigger("ChangeMode");
        if (!canMove)
        {
            canShoot = false;
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * anim.GetCurrentAnimatorStateInfo(0).speed);
        }
        else
        {
            canShoot = true;
        }
        canMove = !canMove;
    }
    

    private void Start()
    {
        startPos = transform.position;

        if (FeedbackController.Instance.hasPlayerVibrating)
            StartCoroutine(ShakePlayer());

        bulletShootParticles.transform.position =
            new Vector2(transform.position.x, transform.position.y + bulletPositionOffset);
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

    public void Shoot()
    {
        if (shootTimer < timeBetweenShoot || !canShoot) return;

        shootTimer = 0f;

        if (shootHeldTimer < timeForLoadShoot)
        {
            Debug.Log("Small Shoot !");
        }
        else
        {
            Debug.Log("Big SHOOT !!");
        }
        GameObject bullet = Instantiate(bulletPrefab);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bullet.transform.position = new Vector2(transform.position.x, transform.position.y + bulletPositionOffset);
        bulletScript.SetDirection(true);
        bulletScript.SetObjectFiring(true);
        bulletScript.SetParameters(spriteOffsetToCenterX, spriteOffsetToCenterY, rotationSpeed);

        if (FeedbackController.Instance.hasShootParticle)
            bulletShootParticles.Play();

        if (FeedbackController.Instance.hasRecoilEffect)
            StartCoroutine(ShootMovement());   
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

    IEnumerator ShootMovement()
    {
        float timer = 0f;
        while (timer < recoilDuration)
        {
            timer += Time.deltaTime;
            Vector2 recoil = new Vector2(transform.position.x, transform.position.y - maxRecoil * recoilCurve.Evaluate(timer / recoilDuration));
            playerSprite.transform.position = recoil;
            yield return null;
        }
        playerSprite.transform.position = transform.position;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetShootHeldTimer(float timer)
    {
        shootHeldTimer = timer;
    }
}
