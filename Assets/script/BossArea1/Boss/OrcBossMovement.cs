using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyState
{
    CHASE,
    ATTACK,
    NONE,
    DIE
}
public class OrcBossMovement : MonoBehaviour
{
    private AnimationController enemy_Anim;
    private NavMeshAgent navAgent;
    private Rigidbody rb;
    private Transform playerTarget;
    private EnemyProperties enemyProperties;
    public float MoveSpeed;

    public float AttackDistance;
    public float ChasePlayerAfterAttackDistance;

    private float AttackWaitTime = 3f;
    private float AttackTimer;

    private Vector3 BaseLocation;
    public EnemyState OrcBossState;

    public bool IsActive;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemy_Anim = GetComponent<AnimationController>();
        navAgent = GetComponent<NavMeshAgent>();
        enemyProperties = GetComponent<EnemyProperties>();
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
        BaseLocation = transform.position;
        //  soundFX = GetComponentInChildren<CharacterSoundFX>();
    }

    void Start()
    {
        OrcBossState = EnemyState.CHASE;
        AttackTimer = AttackWaitTime;
    }

    void Update()
    {

        if (IsActive)
        {
            if (OrcBossState == EnemyState.CHASE && !playerTarget.GetComponent<PlayerProperties>().Isdead && !enemyProperties.Isdead)
            {
                ChasePlayer();
            }
            else if (OrcBossState == EnemyState.DIE)
            {
                Invoke("TeleportPlayerBack", 1f);
            }
            else if (OrcBossState == EnemyState.ATTACK && !playerTarget.GetComponent<PlayerProperties>().Isdead && !enemyProperties.Isdead)
            {
                AttackPlayer();
            }
            else if (OrcBossState == EnemyState.NONE && !enemyProperties.Isdead)
            {
                if (enemyProperties.enemyhealth <= enemyProperties.GetMaxHealth())
                {
                    enemyProperties.enemyhealth += 1;
                    enemyProperties.enemyUI.DisplayHealth(enemyProperties.enemyhealth, enemyProperties.GetMaxHealth());
                    navAgent.isStopped = false;
                    ReturnToBase();
                }
            }

        }
    }


    void ChasePlayer()
    {
        if (!enemyProperties.Isdead)
        {
            navAgent.SetDestination(playerTarget.position);
            navAgent.speed = MoveSpeed;

            if (navAgent.velocity.sqrMagnitude == 0)
            {

                enemy_Anim.SetRun(false);
            }
            else
            {
                enemy_Anim.SetRun(true);
            }
            //print(Vector3.Distance(transform.position, playerTarget.position));
            if (Vector3.Distance(transform.position, playerTarget.position) <= AttackDistance)
            {
                OrcBossState = EnemyState.ATTACK;
            }
            else if (Vector3.Distance(transform.position, playerTarget.position) > 200f)
            {
                OrcBossState = EnemyState.NONE;
            }

        }
    }
    void ReturnToBase()
    {
        if (!enemyProperties.Isdead)
        {
            navAgent.SetDestination(BaseLocation);
            // transform.LookAt(playerTarget.position);
            navAgent.speed = MoveSpeed;

            if (navAgent.velocity.sqrMagnitude == 0)
            {

                enemy_Anim.SetRun(false);
            }
            else
            {
                enemy_Anim.SetRun(true);
            }

        }
    }

    void AttackPlayer()
    {

        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        transform.LookAt(playerTarget.position);
        enemy_Anim.SetRun(false);

        AttackTimer += Time.deltaTime;

        if (AttackTimer > AttackWaitTime)
        {
            int number = Random.Range(1, 10);
            if (number <= 7)
            {
                enemy_Anim.SetAttack(1);
            }
            else
            {
                enemy_Anim.OrcBossSkill2(playerTarget);
            }
            // enemy_Anim.SetAttack(1);

            AttackTimer = 0f;

        }

        else if (Vector3.Distance(transform.position, playerTarget.position) >
            AttackDistance + ChasePlayerAfterAttackDistance)
        {

            navAgent.isStopped = false;
            OrcBossState = EnemyState.CHASE;

        }

    }

    public void SetNoneState()
    {
        OrcBossState = EnemyState.NONE;
    }
    public void SetDieState()
    {
        OrcBossState = EnemyState.DIE;

    }
    public void ActiveNavAgent()
    {
        OrcBossState = EnemyState.CHASE;
    }

    private void TeleportPlayerBack()
    {
        Vector3 ReturnPosition = new Vector3(-3f, 30f, 72f);
        playerTarget.transform.position = ReturnPosition;
    }


}
