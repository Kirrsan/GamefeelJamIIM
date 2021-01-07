using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    public string animationBool;
    private Animator animator;

    public UnityEvent eventToPlayOnAnimation;
    public UnityEvent eventToPlayShootMode;
    public UnityEvent eventToPlayMoveMode;
    public GameObject objectToDestroy;
    
    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
    }

    
    public void SetAnimationBoolFalse()
    {
        animator.SetBool(animationBool, false);
    }


    public void PlayEvent()
    {
        eventToPlayOnAnimation.Invoke();
    }    
    public void PlayEventShootMode()
    {
        eventToPlayShootMode.Invoke();
    }    
    public void PlayEventMoveMode()
    {
        eventToPlayMoveMode.Invoke();
    }
    
    public void DestroyGameObject()
    {
        Destroy(objectToDestroy);
    }
}
