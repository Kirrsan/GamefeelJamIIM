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
        if (Input.GetKey(KeyCode.RightArrow) && playerCharacter.GetCanMove())
        {
            playerCharacter.Move(true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && playerCharacter.GetCanMove())
        {
            playerCharacter.Move(false);
        }
        
        // Mode
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            playerCharacter.SwitchMode();
        }

        // Shoot
        if (Input.GetKey(KeyCode.Space) && !playerCharacter.GetCanMove())
        {
            loadingShootTimer += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) && !playerCharacter.GetCanMove())
        {
            playerCharacter.Shoot(loadingShootTimer);
            loadingShootTimer = 0f;
        }
    }
}
