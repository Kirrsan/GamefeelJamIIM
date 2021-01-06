using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseZoneParticle : MonoBehaviour
{
    public GameObject particlesPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            Vector2 position = this.gameObject.transform.position;
            GameObject particle = Instantiate(particlesPrefab);
            particle.transform.position = position;
            Destroy(particle, 2f);
        }
    }
}
