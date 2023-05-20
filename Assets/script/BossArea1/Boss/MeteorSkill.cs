using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSkill : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float FallSpeed;
    [SerializeField] GameObject HitEffect;
    private Vector3 direction;
    private bool CanMove = true;
    public int damage;
    private AudioSource audioSource;
    public AudioClip HitSound;
    private void Awake()
    {
        print(transform.localPosition);
        //direction = new Vector3(1, -1, 0);
        audioSource = GetComponent<AudioSource>();
        direction = new Vector3(0, -1, 0);
        rb = GetComponent<Rigidbody>();

    }
    void Start()
    {

    }
    /// <summary>
    ///  move in 3.57f by x axis
    ///  base + move = final
    ///  base = player position - 3.57f
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        if (CanMove)
            transform.Translate(direction * FallSpeed * Time.deltaTime);
    }
    void DelayDestroy()
    {
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision target)
    {
        audioSource.PlayOneShot(HitSound);

        CanMove = false;
        rb.freezeRotation = true;
        print(transform.position);

        Instantiate(HitEffect, transform.position, Quaternion.identity);

        if (target.gameObject.CompareTag(Tags.PLAYER_TAG))
        {
            target.gameObject.GetComponent<PlayerProperties>().ApplyDamage(damage);
        }
        Invoke("SetActiveFalse", 0.9f);
        Invoke("DelayDestroy", 1f);
    }
    void SetActiveFalse()
    {
        gameObject.SetActive(false);

    }
}
