using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public bool HasList;
    public AudioClip[] MusicList;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag(Tags.PLAYER_TAG))
        {

            if (HasList)
            {
                int number = Random.Range(0, MusicList.Length);
                audioSource.clip = MusicList[number];
                audioSource.Play();

            }
            else audioSource.Play();
        }
    }
    private void OnTriggerExit(Collider target)
    {
        if (target.CompareTag(Tags.PLAYER_TAG))
        {
            audioSource.Stop();

        }
    }
}
