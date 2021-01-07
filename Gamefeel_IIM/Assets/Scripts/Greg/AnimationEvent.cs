using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public string animationBool;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
    }

    
    public void SetAnimationBoolFalse()
    {
        animator.SetBool(animationBool, false);
    }
}
