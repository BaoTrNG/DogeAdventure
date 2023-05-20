using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkAndDef : MonoBehaviour
{
    public FixedButton atkBtn;
    public FixedButton ShieldBtn;
    public GameObject ShieldAura;

    private playermovement playermovement;
    private AnimationController anim;
    private PlayerProperties playerProperties;
    private float counter = 0f;
    private float delay = 0.02f;

    private bool canAttack = true;
    private bool canDefend = true;
    private bool CanSpawnShieldAura = true;

    [SerializeField] private AudioClip ShieldSound;
    private AudioSource audioSource;
    private bool IsOnMusic = false;


    private bool ManaMinusNow = true;


    private int ShieldManaUse = 5;
    private int ShieldManaUseOverTime = 10;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playermovement = GetComponent<playermovement>();
        anim = GetComponent<AnimationController>();
        playerProperties = GetComponent<PlayerProperties>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerAtk();
        PlayerDef();

    }

    void PlayerAtk()
    {
        if (Input.GetKeyDown(KeyCode.K) && canAttack && playermovement.isGrounded && !playerProperties.Isdead)
        {
            // print("first " + playermovement.CanMove);
            playermovement.CanMove = false;
            int atk = Random.Range(1, 3);
            anim.SetAttack(atk);
            canAttack = false;
            canDefend = false;
            Invoke("ResetCanAttack", 1f);
            // print("before " + playermovement.CanMove);

        }
        else if (atkBtn.Pressed && canAttack && playermovement.isGrounded && !playerProperties.Isdead)
        {
            // atkBtn.Pressed = false;
            playermovement.CanMove = false;
            int atk = Random.Range(1, 3);
            anim.SetAttack(atk);
            canAttack = false;
            canDefend = false;
            Invoke("ResetCanAttack", 1f);
        }
    }
    void PlayerDef()
    {

        if (ShieldBtn.Pressed && canDefend && playermovement.isGrounded && !playerProperties.Isdead)
        {
            if (playerProperties.GetMana() >= ShieldManaUse)
            {
                if (CanSpawnShieldAura)
                {
                    CanSpawnShieldAura = false;
                    Instantiate(ShieldAura, transform.position, Quaternion.identity);
                }


                playerProperties.playerUi.PlayerInfo.text = "Guarding";
                playerProperties.playerUi.PlayerInfo.color = Color.green;

                if (!IsOnMusic)
                {
                    audioSource.clip = ShieldSound;
                    audioSource.Play();
                    audioSource.loop = true;
                    IsOnMusic = true;
                }


                // playermovement._rigidbody.isKinematic = true;  bug : map sound box collider on trigger event restart
                playerProperties.MinusMana(ShieldManaUse, ManaMinusNow);
                playerProperties.MinusManaOverTime(ShieldManaUseOverTime);
                anim.SetDefend(true);
                playermovement.CanMove = false;
                canAttack = false;
                ManaMinusNow = false;
                playerProperties.IsDefend = true;



            }
            else
            {

                ResetAfterShield();
            }

        }
        else if (ShieldBtn.WasPressed())
        {

            ResetAfterShield();
        }
    }
    void ResetAfterShield()
    {
        playerProperties.playerUi.PlayerInfo.text = "";

        IsOnMusic = false;
        audioSource.Stop();
        audioSource.loop = false;


        // playermovement._rigidbody.isKinematic = false;
        CanSpawnShieldAura = true;
        Destroy(GameObject.FindWithTag("ShieldAura"));
        playerProperties.AllowRegenMana();
        anim.SetDefend(false);
        playermovement.CanMove = true;
        canAttack = true;
        ManaMinusNow = true;
        playerProperties.IsDefend = false;
    }
    void ResetCanAttack()
    {
        canAttack = true;
        canDefend = true;
        playermovement.CanMove = true;
    }
}
