using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseZoneParticle : MonoBehaviour
{
    public GameObject particlesPrefab;
    public GameObject oilPrefabToSetActive;

    private bool isOilPrefabActive;

    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            if (FeedbackController.Instance.hasSparksEffect)
            {
                if (!isOilPrefabActive)
                {
                    oilPrefabToSetActive.SetActive(true);
                }
                Vector2 position = this.gameObject.transform.position;
                GameObject particle = Instantiate(particlesPrefab);
                particle.transform.position = position;
                Destroy(particle, 2f);
            }
            
            if (FeedbackController.Instance.hasCrackEffect)
                UIManager.Instance.AddCrack();
            
            
        }
    }
}
