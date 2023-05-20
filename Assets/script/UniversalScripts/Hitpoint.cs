using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hitpoint : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject HitEffect;
    public LayerMask collisionLayer;
    public float radius = 0.33f;
    public int damage = 2;

    public bool is_Player, is_Enemy, is_Guard, is_Boss;
    private AudioSource audioSource;
    public AudioClip hitsound;

    // private bool IsPlayingSound = false;
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

        //DetectCollision();

    }
    void FixedUpdate()
    {
        DetectCollision();
    }
    void DetectCollision()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);
        if (hit.Length > 0)
        {

            if (is_Player)
            {
                if (hit[0].CompareTag(Tags.ORC_BOSS_TAG))
                {
                    Vector3 effectLocation = transform.position;
                    effectLocation.y += 3.5f;
                    int damage = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerProperties>().damage;
                    if (hit[0].GetComponent<EnemyProperties>().enemyhealth > 0)
                    {
                        hit[0].GetComponent<EnemyProperties>().ApplyDamage(damage);
                        if (hit[0].GetComponent<EnemyProperties>().enemyhealth <= 0)
                        {
                            hit[0].GetComponent<OrcBossMovement>().SetDieState();
                        }
                    }

                    Instantiate(HitEffect, effectLocation, Quaternion.identity);
                }
                else
                {
                    for (int i = 0; i < hit.Length; i++)
                    {

                        //  int damage = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerProperties>().damage;
                        if (hit[i].GetComponent<EnemyProperties>().enemyhealth > 0)
                        {
                            hit[i].GetComponent<EnemyProperties>().ApplyDamage(parent.GetComponent<PlayerProperties>().damage);
                            if (hit[i].GetComponent<EnemyProperties>().enemyhealth <= 0)
                            {
                                parent.GetComponent<PlayerProperties>().CheckForQuest(hit[i].GetComponent<EnemyProperties>().GetEnemyName());
                            }
                            hit[i].GetComponent<EnemyMovement>().ActiveNavAgent();
                            hit[i].GetComponent<AnimationController>().SetGetHit();
                            Vector3 HitEffectLocation = hit[i].transform.position;
                            HitEffectLocation.y += 1.5f;
                            Instantiate(HitEffect, HitEffectLocation, Quaternion.identity);
                        }

                    }
                }

            } // if is player
            else if (is_Enemy)
            {
                PlayerProperties playerProperties = hit[0].GetComponent<PlayerProperties>();
                playerProperties.ApplyDamage(parent.GetComponent<EnemyProperties>().damage);
                if (playerProperties.Isdead) parent.GetComponent<EnemyMovement>().SetNoneState();

            }
            else if (is_Boss)
            {
                print("hit player");
                PlayerProperties playerProperties = hit[0].GetComponent<PlayerProperties>();
                playerProperties.ApplyDamage(parent.GetComponent<EnemyProperties>().damage);
                if (playerProperties.Isdead) parent.GetComponent<OrcBossMovement>().SetNoneState();
                gameObject.SetActive(false);


            }
            else if (is_Guard)
            {

            }
            gameObject.SetActive(false);

        } //


    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
