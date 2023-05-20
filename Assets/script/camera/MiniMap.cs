using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Transform PlayerTransform;
    public float height = 100;
    private void Awake()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 NewPostion = PlayerTransform.position;
        NewPostion.y = height;
        transform.position = NewPostion;
        // transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
