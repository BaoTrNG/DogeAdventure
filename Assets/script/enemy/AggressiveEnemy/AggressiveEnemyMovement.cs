using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AggressiveEnemyMovement : MonoBehaviour
{
    private Vector3 TargetLocation;
    private Vector3 BaseLocation;
    private NavMeshAgent navAgent;
    private EnemyProperties enemyProperties;
    private AnimationController enemy_Anim;
    private Rigidbody rb;
    private Transform playerTarget;
    public float move_Speed;
    public float attack_Distance;
    public float SafeRange;
    public float chase_Player_After_Attack_Distance;


    private float wait_Before_Attack_Time = 2f;
    private float attack_Timer;

    private EnemyState enemy_State;
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        enemyProperties = GetComponent<EnemyProperties>();
        enemy_Anim = GetComponent<AnimationController>();
        rb = GetComponent<Rigidbody>();
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
        BaseLocation = transform.position;
    }
    void Start()
    {
        enemy_State = EnemyState.NONE;
        attack_Timer = wait_Before_Attack_Time;
    }

    // Update is called once per frame
    void Update()
    {
        print(enemy_State);
        if (enemy_State == EnemyState.CHASE && playerTarget.GetComponent<PlayerProperties>().Isdead == false)
        {
            AggressiveChasePlayer();
        }
        else if (enemy_State == EnemyState.ATTACK && playerTarget.GetComponent<PlayerProperties>().Isdead == false)
        {

        }
        else if (enemy_State == EnemyState.NONE)
        {
        }
    }

    void AggressiveChasePlayer()
    {
        if (!enemyProperties.Isdead)
        {
            navAgent.SetDestination(TargetLocation);
            navAgent.speed = move_Speed;

            if (navAgent.velocity.sqrMagnitude != 0)
            {
                enemy_Anim.SetRun(true);
            }
            else
            {
                enemy_Anim.SetRun(false);
            }


            if (Vector3.Distance(transform.position, playerTarget.position) >= SafeRange)
            {
                enemy_State = EnemyState.NONE;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                ReturnToBase();


            }
        }

    }

    void ReturnToBase()
    {
        navAgent.SetDestination(BaseLocation);
        navAgent.speed = move_Speed;
        if (Vector3.Distance(transform.position, BaseLocation) <= 1f)
        {
            enemy_State = EnemyState.NONE;
            enemy_Anim.SetRun(false);

        }
    }
    /*  void AttackPlayer()
      {

          navAgent.velocity = Vector3.zero;
          navAgent.isStopped = true;

          enemy_Anim.SetRun(false);

          attack_Timer += Time.deltaTime;

          if (attack_Timer > wait_Before_Attack_Time)
          {

              enemy_Anim.SetAttack(2);

              attack_Timer = 0f;

          } // if we can attack

          else if (Vector3.Distance(transform.position, playerTarget.position) > attack_Distance + chase_Player_After_Attack_Distance)
          {

              navAgent.isStopped = false;
              enemy_State = EnemyState.CHASE;

          }

      }*/

    void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag(Tags.PLAYER_TAG))
        {
            print("collide with player");
            enemy_State = EnemyState.CHASE;
            TargetLocation = target.transform.position;
        }
    }
    void OnTriggerExit(Collider target)
    {
        if (target.CompareTag(Tags.PLAYER_TAG))
        {
            print("return to base");
            TargetLocation = BaseLocation;
        }
    }
}
