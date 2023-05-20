using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasLockRotation : MonoBehaviour
{
    private Quaternion baserotation;
    private void Awake()
    {

    }
    void Start()
    {
        baserotation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = baserotation;
    }


}
