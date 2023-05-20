using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveGameObject : MonoBehaviour
{
    [SerializeField] private float time;
    void Start()
    {
        Invoke("Deactive", time);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Deactive()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
