using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class followplayer : MonoBehaviour
{
    public float _rotateSpeed;
    public FloatingJoystick floatingJoystick;
    private Vector3 rotatevector;
    private Transform target;
    private Vector3 velocity = Vector3.zero;

    public float smoothTime = 0.3f;
    public Vector3 offset = new Vector3(0, 10, -11);


    private AudioSource audioSource;
    public AudioClip ost;
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //   FollowPlayer();
        FollowPlayerSmooth();

    }
    void LateUpdate()
    {
        // FollowPlayerSmooth();
    }

    void PlayMusic()
    {
        audioSource.volume = 0.5f;
        audioSource.loop = true;
        audioSource.clip = ost;
        audioSource.Play();
    }
    void FollowPlayer()
    {
        Vector3 offset = target.position;
        offset.y += 10f;
        offset.x += 0f;
        offset.z -= 11f;
        transform.position = offset;

    }
    void FollowPlayerSmooth()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }



}
