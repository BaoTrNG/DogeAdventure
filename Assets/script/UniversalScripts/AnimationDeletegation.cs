using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDeletegation : MonoBehaviour
{
    public GameObject Hitpoint;
    public AudioClip SwordWhoosh;
    public AudioClip WalkSound;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //  Hitpoint = GameObject.FindWithTag("HitPoint");
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    void PlayWalkSound()
    {
        audioSource.volume = 0.6f;
        audioSource.PlayOneShot(WalkSound);
    }
    void PlaySwordWhooshSound()
    {
        audioSource.volume = 1f;
        audioSource.PlayOneShot(SwordWhoosh);
    }
    void ActiveHitPoint()
    {
        Hitpoint.SetActive(true);
    }

    void DeactiveHitPoint()
    {
        if (Hitpoint.activeInHierarchy)
        {
            Hitpoint.SetActive(false);
        }
    }
}
