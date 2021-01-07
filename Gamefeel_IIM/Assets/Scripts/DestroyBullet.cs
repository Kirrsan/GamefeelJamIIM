using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet")) return;
        Debug.Log("Bullet destruction");
        Destroy(collision.gameObject.transform.parent.gameObject);
    }
}
