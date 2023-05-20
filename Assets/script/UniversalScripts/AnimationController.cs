using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    //public GameObject SlashEffect;
    private Animator _animator;
    private AudioSource audioSource;
    public AudioClip hitsound;
    public GameObject OrcHitBossEffect;
    public GameObject OrcBossMeteorite;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRun(bool run)
    {
        _animator.SetBool("IsMove", run);
    }

    public void SetJump(bool jump)
    {
        _animator.SetBool("IsJump", jump);
    }

    public void SetAttack(int atk)
    {

        if (atk == 1)
        {
            _animator.SetTrigger("atk1");
        }
        else if (atk == 2)
        {
            _animator.SetTrigger("atk2");
        }
    }

    public void OrcBossSkill2(Transform PlayerPostion)
    {

        _animator.SetTrigger("atk2");
        Vector3 spawnPosition = PlayerPostion.position;
        spawnPosition.y = 25f;
        for (int i = 0; i <= 1; i++)
        {
            Instantiate(OrcBossMeteorite, spawnPosition, Quaternion.identity);
            spawnPosition.y += 5f;
            spawnPosition.x += Random.Range(-3f, 3f);
            spawnPosition.z += Random.Range(-3f, 3f);

        }

    }
    public void SetDefend(bool defend)
    {
        _animator.SetBool("IsDefend", defend);
    }

    public void SetDead()
    {
        _animator.SetTrigger("Dead");
    }

    public void SetIsDead(bool isdead)
    {
        _animator.SetBool("IsDead", isdead);
    }
    public void SetTriggerIsDead()
    {
        _animator.SetTrigger("TriggerIsDead");
    }
    public void SpawnOrcBossHitEffect()
    {
        Vector3 SpawnLocation = transform.localPosition + transform.forward * 6f;
        Instantiate(OrcHitBossEffect, SpawnLocation, Quaternion.identity);
        audioSource.volume = 1f;
        audioSource.PlayOneShot(hitsound);
    }
    public void SetGetHit()
    {
        audioSource.volume = 1f;
        // audioSource.clip = hitsound;
        audioSource.PlayOneShot(hitsound);
        //_animator.SetTrigger("hit"); bugg
    }

}
