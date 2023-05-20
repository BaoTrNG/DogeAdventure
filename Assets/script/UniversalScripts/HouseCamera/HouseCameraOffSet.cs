using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseCameraOffSet : MonoBehaviour
{
    public Vector3 Offset = new Vector3(0, 7, -2);
    [SerializeField] private followplayer MainCamera;
    private Vector3 BaseOffSet;
    void Awake()
    {
        BaseOffSet = MainCamera.offset;

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider target)
    {
        MainCamera.offset = Offset;
    }
    void OnTriggerExit(Collider target)
    {
        MainCamera.offset = BaseOffSet;
    }
}
