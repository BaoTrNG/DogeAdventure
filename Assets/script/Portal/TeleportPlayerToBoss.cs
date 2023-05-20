using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeleportPlayerToBoss : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private GameObject PortalBack;
    [SerializeField] private TextMeshProUGUI Warning;
    private Vector3 TeleportPos;
    [SerializeField] bool Forward;
    private bool IsTeleport = false;
    private GameObject player;
    private int count = 0;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    void Start()
    {
        //   Invoke("Teleport", 3f);
        TeleportPos = Target.position;
        if (Forward)
        {
            TeleportPos.x -= 2f;
        }
        else
        {
            TeleportPos.x += 2f;
        }
    }


    // Update is called once per frame
    void Update()
    {

        //print(Target.position);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.PLAYER_TAG && !IsTeleport)
        {

            //collision.gameObject.transform.position = TeleportPos;
            Invoke("Teleport", 0.2f);

            IsTeleport = true;
            Invoke("ResetIsTeleport", 3f);
            ResetBossItem();

        }
    }

    public void ResetBossItem()
    {
        PortalBack.SetActive(true);

        Warning.text = "Safe Zone Once You Get Out, Portal will disappear and Boss Battle Start";
        Warning.color = new Color(255, 255, 0);
    }
    void ResetIsTeleport()
    {
        IsTeleport = false;
    }

    void Teleport()
    {
        //  player.transform.position = Target.position;
        player.transform.position = TeleportPos;
    }
}
