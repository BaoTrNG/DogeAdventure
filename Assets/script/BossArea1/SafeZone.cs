using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SafeZone : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI Warning;
    [SerializeField] private GameObject Portal;
    [SerializeField] private Transform BossLocation;
    [SerializeField] private GameObject Boss;
    private GameObject BossObject;
    private OrcBossMovement ActiveBoss;
    void Awake()
    {

        //    ActiveBoss = GameObject.FindGameObjectWithTag(Tags.ORC_BOSS_TAG).GetComponent<OrcBossMovement>();
        //  Warning = GameObject.FindWithTag(Tags.BOSS_WARNING_TEXT).GetComponent<TextMeshProUGUI>();


    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {

        if (BossObject == null)
        {
            BossObject = Instantiate(Boss, BossLocation.position, Quaternion.Euler(0, 180, 0));
            ActiveBoss = GameObject.FindGameObjectWithTag(Tags.ORC_BOSS_TAG).GetComponent<OrcBossMovement>();
            Warning = GameObject.FindWithTag(Tags.BOSS_WARNING_TEXT).GetComponent<TextMeshProUGUI>();
            Warning.text = "Safe Zone Once You Get Out, Portal will disappear and Boss Battle Start";
            ActiveBoss.OrcBossState = EnemyState.NONE;
            ActiveBoss.IsActive = false;
        }
    }
    void OnTriggerExit(Collider target)
    {
        if (target.CompareTag(Tags.PLAYER_TAG))
        {
            Warning.text = "";
            Portal.SetActive(false);
            ActiveBoss.IsActive = true;
            ActiveBoss.OrcBossState = EnemyState.CHASE;

        }
    }
}
