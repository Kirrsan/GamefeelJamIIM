using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public Sprite sprite;
    private float spriteHeight;
    public float scrollingSpeed;
    

    public GameObject[] backgrounds;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteHeight = sprite.bounds.size.y;
    }

    public void ChangePosition(int ID)
    {
        if (ID != 0)
        {
            backgrounds[ID].transform.position =
                new Vector2(backgrounds[ID-1].transform.position.x,
                    backgrounds[ID-1].transform.position.y - spriteHeight* 0.5f);
        }
        else
        {
            backgrounds[ID].transform.position =
                new Vector2(backgrounds[backgrounds.Length - 1].transform.position.x,
                    backgrounds[backgrounds.Length - 1].transform.position.y - spriteHeight * 0.5f);
        }

    }
}
