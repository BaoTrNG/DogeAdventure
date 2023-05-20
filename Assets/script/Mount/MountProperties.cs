using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountProperties : MonoBehaviour
{
    private Rigidbody rb;
    private AnimationController anim;
    public float MountSpeed = 10f;
    void Awake()
    {
        anim = GetComponent<AnimationController>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
