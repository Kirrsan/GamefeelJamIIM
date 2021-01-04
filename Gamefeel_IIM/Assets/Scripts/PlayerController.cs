using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerCharacter playerCharacter;

    [SerializeField] float loadingShootTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        // Movement
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerCharacter.Move(true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerCharacter.Move(false);
        }

        // Shoot
        if (Input.GetKey(KeyCode.Space))
        {
            loadingShootTimer += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerCharacter.Shoot(loadingShootTimer);
            loadingShootTimer = 0f;
        }
    }
}
