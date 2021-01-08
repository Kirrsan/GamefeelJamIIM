using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource audioSource;

    public AudioClip PlayerShootSound;
    public AudioClip EnnemyImpact;
    public AudioClip PlayerImpact;
    public AudioClip EnemyShoot;
    public AudioClip ShieldBlock;
    public AudioClip EnnemyFall;
    public AudioClip SwitchMode;
    public AudioClip EngineStart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        //audioClip.Play();
    }
}
