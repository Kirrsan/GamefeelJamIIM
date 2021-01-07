using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerCharacter playerCharacter;

    [SerializeField] float loadingShootTimer = 0f;
    [SerializeField] Animator shipAnim;

    // Update is called once per frame
    void Update()
    {
        // Movement
        if (Input.GetKey(KeyCode.RightArrow) && playerCharacter.GetCanMove())
        {
            playerCharacter.Move(true);
            shipAnim.SetInteger("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && playerCharacter.GetCanMove())
        {
            playerCharacter.Move(false);
            shipAnim.SetInteger("Direction", -1);
        }
        else
        {
            shipAnim.SetInteger("Direction", 0);
        }
        
        // Mode
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            playerCharacter.StartCoroutine(playerCharacter.SwitchMode(shipAnim));
        }

        // Shoot
        if (Input.GetKey(KeyCode.Space) && !playerCharacter.GetCanMove())
        {
            loadingShootTimer += Time.deltaTime;
            playerCharacter.SetShootHeldTimer(loadingShootTimer);
        }

        if (Input.GetKeyUp(KeyCode.Space) && !playerCharacter.GetCanMove())
        {
            if (shipAnim.GetBool("Shoot")) return;
            // playerCharacter.Shoot(loadingShootTimer);
            shipAnim.SetBool("Shoot",true);
            loadingShootTimer = 0f;
            playerCharacter.SetShootHeldTimer(loadingShootTimer);
        }
    }
}
