using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum GuardState
{
    MOVE,
    ATTACK,
    NONE
}
public class GuardMovement : MonoBehaviour
{
    private NavMeshAgent meshAgent;
    private AnimationController anim;
    public List<Transform> Destination;
    private bool CanMove = true;
    [SerializeField] private bool IsWander;
    private int number;
    GuardState guardState;
    [SerializeField] Canvas GuardInfo;
    private void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<AnimationController>();
        number = Random.Range(0, Destination.Count);
    }
    void Start()
    {
        guardState = GuardState.MOVE;
    }

    // Update is called once per frame
    void Update()
    {
        if (guardState == GuardState.MOVE && IsWander)
        {
            WalkToRandomDestion(number);
        }
    }

    void WalkToRandomDestion(int number)
    {
        //
        meshAgent.SetDestination(Destination[number].position);
        meshAgent.speed = 5f;

        if (meshAgent.velocity.magnitude > 0)
        {
            anim.SetRun(true);
        }

        if (Vector3.Distance(transform.position, Destination[number].position) <= 5f)
        {
            anim.SetRun(false);
            guardState = GuardState.NONE;
            meshAgent.SetDestination(transform.position);
            Invoke("WaitBeforeNextDestination", 3f);

        }

    }

    void WaitBeforeNextDestination()
    {
        if (guardState == GuardState.NONE)
        {
            number = Random.Range(0, Destination.Count);
            guardState = GuardState.MOVE;

        }

    }

}
