using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private AnimationController enemy_Anim;
    private NavMeshAgent navAgent;
    private Rigidbody rb;
    private Transform playerTarget;
    private EnemyProperties enemyProperties;
    public float move_Speed;

    public float attack_Distance;
    public float SafeRange;
    public float chase_Player_After_Attack_Distance;

    private float wait_Before_Attack_Time = 2f;
    private float attack_Timer;

    private Vector3 BaseLocation;
    private EnemyState enemy_State;
    private bool ReturnBase;

    [SerializeField] private bool IsAggressive;




    // public GameObject attackPoint;

    // private CharacterSoundFX soundFX;

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
        if (IsAggressive) enemy_State = EnemyState.CHASE;
        else enemy_State = EnemyState.NONE;

        attack_Timer = wait_Before_Attack_Time;
    }

    // Update is called once per frame
    void Update()
    {
        print("enemy state");
        if (enemy_State == EnemyState.CHASE && playerTarget.GetComponent<PlayerProperties>().Isdead == false)
        {
            if (IsAggressive)
            {

                AggressiveChasePlayer();
            }

            else ChasePlayer();

        }
        else if (enemy_State == EnemyState.ATTACK && playerTarget.GetComponent<PlayerProperties>().Isdead == false)
        {
            AttackPlayer();
        }

        else if (enemy_State == EnemyState.NONE)
        {
            if (enemyProperties.enemyhealth <= enemyProperties.GetMaxHealth())
            {
                enemyProperties.enemyhealth += 1;
                enemyProperties.enemyUI.DisplayHealth(enemyProperties.enemyhealth, enemyProperties.GetMaxHealth());

            }
            if (Vector3.Distance(transform.position, BaseLocation) > 3f)
            {
                ReturnToBase();
            }
            else if (Vector3.Distance(transform.position, BaseLocation) < 3f)
            {

                enemy_Anim.SetRun(false);
            }
        }
    }
    public void SetNoneState()
    {
        enemy_State = EnemyState.NONE;
    }
    void ReturnToBase()
    {

        ReturnBase = true;
        navAgent.SetDestination(BaseLocation);
        navAgent.speed = move_Speed;
        enemy_Anim.SetRun(true);

    }
    void ChasePlayer()
    {
        if (!enemyProperties.Isdead)
        {

            ReturnBase = false;
            navAgent.SetDestination(playerTarget.position);
            transform.LookAt(playerTarget.position);
            navAgent.speed = move_Speed;

            if (navAgent.velocity.sqrMagnitude == 0)
            {

                enemy_Anim.SetRun(false);
            }
            else
            {
                enemy_Anim.SetRun(true);
            }

            if (Vector3.Distance(transform.position, playerTarget.position) >= SafeRange)
            {
                enemy_State = EnemyState.NONE;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                // ReturnToBase();
                //enemy_Anim.SetRun(false);

            }
            else if (Vector3.Distance(transform.position, playerTarget.position) <= attack_Distance)
            {
                enemy_State = EnemyState.ATTACK;
            }
        }
    }
    void AggressiveChasePlayer()
    {
        print("aggressive");
        if (!enemyProperties.Isdead)
        {

            if (Vector3.Distance(transform.position, playerTarget.position) < SafeRange)
            {
                print("chase");
                navAgent.SetDestination(playerTarget.position);
                navAgent.speed = move_Speed;

                if (navAgent.velocity.sqrMagnitude == 0)
                {

                    enemy_Anim.SetRun(false);
                }
                else
                {

                    enemy_Anim.SetRun(true);

                }

                if (Vector3.Distance(transform.position, playerTarget.position) <= attack_Distance)
                {
                    enemy_State = EnemyState.ATTACK;
                }

            }
            else if (Vector3.Distance(transform.position, playerTarget.position) > SafeRange)
            {
                enemy_State = EnemyState.NONE;
            }
        }
    }

    void AttackPlayer()
    {
        transform.LookAt(playerTarget.position);
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        enemy_Anim.SetRun(false);

        attack_Timer += Time.deltaTime;

        if (attack_Timer > wait_Before_Attack_Time)
        {

            enemy_Anim.SetAttack(2);

            attack_Timer = 0f;

        }

        else if (Vector3.Distance(transform.position, playerTarget.position) >
            attack_Distance + chase_Player_After_Attack_Distance)
        {

            navAgent.isStopped = false;
            enemy_State = EnemyState.CHASE;

        }

    }



    public void ActiveNavAgent()
    {
        enemy_State = EnemyState.CHASE;
    }


}
