using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    public int ID;
    public ScrollingBackground scrollingBackgroundScript;
    private float speed;

    private void Start()
    {
        speed = scrollingBackgroundScript.scrollingSpeed;
    }

    private void Update()
    {
        Vector2 pos = transform.position;
        pos.y += speed * Time.deltaTime;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("endScroll"))
        {
            scrollingBackgroundScript.ChangePosition(ID);
        }
    }
}
