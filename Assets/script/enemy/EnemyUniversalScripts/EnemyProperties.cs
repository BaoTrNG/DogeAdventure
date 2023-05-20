using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    [SerializeField] private string EnemyName;
    public int enemyhealth;
    [SerializeField] private int maxhealth;
    [SerializeField] private float level;
    [SerializeField] private int exp;

    public int damage;
    public bool Isdead;

    public EnemyUI enemyUI;
    private GameObject player;
    private AnimationController anim;
    private Rigidbody rb;
    public string GetEnemyName()
    {
        return EnemyName;
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag(Tags.PLAYER_TAG);
        enemyUI = GetComponent<EnemyUI>();
        enemyhealth = maxhealth;
        anim = GetComponent<AnimationController>();
    }
    void Start()
    {
        enemyUI.DisplayLv(level, EnemyName);
        ApplyDamage(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int GetMaxHealth()
    {
        return maxhealth;
    }

    public void ApplyDamage(int damage)
    {
        if (Isdead)
        {
            return;
        }

        enemyhealth -= damage;

        if (enemyhealth <= 0)
        {
            Isdead = true;
            anim.SetIsDead(true);
            rb.isKinematic = true;
            enemyhealth = 0;
            player.GetComponent<PlayerProperties>().AddExp(exp);
            enemyUI.DisplayHealth(enemyhealth, maxhealth);
            Invoke("DestroyEnemy", 2f);
            //gameObject.SetActive(false);

        }
        else enemyUI.DisplayHealth(enemyhealth, maxhealth);
    }
    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
