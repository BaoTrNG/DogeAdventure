using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [SerializeField] private Transform PlayerRespawn;
    private PlayerSaveLoad SaveGame;
    [SerializeField] private int Health;
    [SerializeField] private int maxHealth = 100;

    [SerializeField] private int HealthRegenSpeed;

    [SerializeField] private GameObject LevelUpEffect;

    [SerializeField] private int BaseDamage = 5;
    public int damage = 5;

    [SerializeField] private int mana;
    [SerializeField] private int maxMana;
    [SerializeField] private int ManaRegen;



    [SerializeField] private int BaseManaRegen = 3;
    [SerializeField] private int BaseHealthRegen = 1;

    private bool IsAllowRegenMana = true;
    private float ManaRegenCounter = 0;
    private float counter = 0;




    public int level = 1;
    public int CurrentExp = 0;
    public int RequireExp = 50;

    public float multiplier = 1.1f;

    public PlayerUi playerUi;
    public bool Isdead;
    public bool IsDefend;




    private AnimationController anim;
    private playermovement playermovement;
    private Rigidbody rb;
    private Transform playertransform;
    private PlayerSaveLoad LoadGameEvent;


    [SerializeField] private TextMeshProUGUI ActiveQuestUI;
    [SerializeField] private PlayerQuestUI QuestUI;
    // public List<GameObject> QuestList = new List<GameObject>();
    public List<Quest> QuestList2 = new List<Quest>();

    private void Awake()
    {
        Health = maxHealth;
        mana = maxMana;
        damage = BaseDamage * level;

        HealthRegenSpeed = BaseHealthRegen;
        ManaRegen = BaseManaRegen;

        playertransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        playerUi = GetComponent<PlayerUi>();
        anim = GetComponent<AnimationController>();
        playermovement = GetComponent<playermovement>();
        SaveGame = GetComponent<PlayerSaveLoad>();
        DisplayActiveQuest();

    }

    void Start()
    {
        ApplyDamage(0);
        playerUi.DisplayExp(CurrentExp, RequireExp, level);

    }

    // Update is called once per frame
    void Update()
    {
        RegenMana();
        // DisplayActiveQuest();
    }
    public void PublicSaveGame()
    {
        SaveGame.Save();
    }

    public void AddMainQuest(GameObject Quest)
    {
        foreach (Quest CheckDuplicate in QuestList2)
        {
            if (CheckDuplicate.QuestID == Quest.GetComponent<Quest>().QuestID)
            {
                return;
            }
        }

        Quest temp = new Quest();
        temp.QuestID = Quest.GetComponent<Quest>().QuestID;
        temp.SetGiverName(Quest.GetComponent<Quest>().GetGiverName());
        temp.ShortDescription = Quest.GetComponent<Quest>().ShortDescription;
        temp.AmountDoneString = Quest.GetComponent<Quest>().GetCount().ToString() + "/" + Quest.GetComponent<Quest>().GetTargetAmount().ToString();
        temp.Description = Quest.GetComponent<Quest>().Description;
        temp.SetExpReward(Quest.GetComponent<Quest>().GetExpReward());
        temp.SetGold(Quest.GetComponent<Quest>().GetGold());
        temp.SetQuestType(Quest.GetComponent<Quest>().GetQuestType());
        temp.SetTargetName(Quest.GetComponent<Quest>().GetTargetName());
        temp.SetTargetAmount(Quest.GetComponent<Quest>().GetTargetAmount());
        QuestList2.Insert(0, temp);


        SaveGame.Save();
        DisplayActiveQuest();


    }




    public void CheckForQuest(string TargetName)
    {

        foreach (Quest current in QuestList2)
        {
            if (current.GetQuestType() == 1)
            {
                print("quest type 1");
                if (current.GetTargetName() == TargetName)
                {
                    print(current.GetTargetAmount());
                    print(current.GetCount());
                    if (current.GetCount() < current.GetTargetAmount())
                    {
                        print("smaller than");
                        current.AddCount();
                        current.SetAmountDoneString();
                        print("this is amount done " + current.AmountDoneString);
                    }

                    if (current.GetTargetAmount() == current.GetCount())
                    {
                        current.IsDone = true;
                    }
                    SaveGame.Save();
                    DisplayActiveQuest();
                }
            }

        }
    }

    public void DisplayActiveQuest()
    {
        if (QuestList2.Count > 0)
        {
            string QuestStatus;
            if (QuestList2[0].IsDone) QuestStatus = "Completed";
            else QuestStatus = "In Progress";


            QuestUI.QuestDescription = QuestList2[0].ShortDescription + " " + QuestList2[0].GetCount() + "/" + QuestList2[0].GetTargetAmount() + "\n" + QuestStatus;
            QuestUI.QuestText.text = QuestUI.QuestDescription;
            QuestUI.FullQuestDescription = QuestList2[0].ShortDescription + " " + QuestList2[0].GetCount() + "/" + QuestList2[0].GetTargetAmount() + "\n \n" + QuestList2[0].Description;


        }
        else ActiveQuestUI.text = "No Quest";

    }
    public void SetProperties(int maxhealth, int maxmana, int healthregen, int manaregen, int level, int requireexp, int currentexp, List<PlayerQuest> QuestList)
    {
        this.maxHealth = maxhealth;
        this.Health = maxhealth;
        this.maxMana = maxmana;
        this.mana = maxmana;
        this.HealthRegenSpeed = healthregen;
        this.ManaRegen = manaregen;
        this.level = level;
        this.RequireExp = requireexp;
        this.CurrentExp = currentexp;
        this.damage = level * BaseDamage;

        if (QuestList.Count > 0)
        {
            foreach (PlayerQuest temp in QuestList)
            {
                Quest quest = new Quest();
                quest.QuestID = temp.QuestID;
                quest.SetGiverName(temp.GiverName);
                quest.IsDone = temp.IsDone;
                quest.ShortDescription = temp.ShortDescription;
                quest.AmountDoneString = temp.AmountDoneString;
                quest.Description = temp.Description;
                quest.SetExpReward(temp.ExpReward);
                quest.SetGold(temp.Gold);
                quest.SetQuestType(temp.QuestType);
                quest.SetCurrentStep(temp.CurrentStep);
                quest.SetTargetName(temp.TargetName);
                quest.SetTargetAmount(temp.TargetAmount);
                quest.SetCount(temp.Count);
                QuestList2.Add(quest);

            }

            // print(QuestList2[0].GetCount());
        }



        DisplayActiveQuest();
        ApplyDamage(0);
        DisplayMana();
        playerUi.DisplayExp(currentexp, requireexp, level);
    }
    public int GetHealthRegenSpeed()
    {
        return HealthRegenSpeed;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetHealth()
    {
        return Health;
    }
    public int GetMaxMana()
    {
        return maxMana;

    }
    public int GetMana()
    {
        return mana;
    }
    public void AddMana(int value)
    {
        mana += value;
    }
    public int GetManaRegenSpeed()
    {
        return ManaRegen;
    }
    public void MinusMana(int consume)
    {
        mana -= consume;
        if (mana <= 0)
        {
            mana = 0;
        }
        DisplayMana();
    }
    public void MinusMana(int consume, bool overtime)
    {
        if (overtime)
        {
            mana -= consume;
            if (mana <= 0)
            {
                mana = 0;
            }
            DisplayMana();
        }
    }

    public void MinusManaOverTime(int consume)
    {
        IsAllowRegenMana = false;
        counter += Time.deltaTime;
        if (counter >= 1f)
        {
            mana -= consume;

            if (mana <= 0)
            {
                mana = 0;
            }
            DisplayMana();
            counter = 0;
        }
    }
    public void AllowRegenMana()
    {
        IsAllowRegenMana = true;
    }
    public void RegenMana()
    {
        if (IsAllowRegenMana)
        {
            ManaRegenCounter += Time.deltaTime;
            if (ManaRegenCounter >= 1f)
            {
                if (mana >= maxMana)
                {
                    mana = maxMana;
                }
                else
                {
                    mana += ManaRegen;
                }
                DisplayMana();
                RegenHealth(false);
                ManaRegenCounter = 0f;
            }

        }
    }

    private void DisplayMana()
    {
        playerUi.DisplayMana(mana, maxMana);
    }

    public void RegenHealth(bool IsOnAura)
    {
        if (Health < maxHealth)
        {
            if (IsOnAura)
            {
                Health += HealthRegenSpeed * 10;
                if (Health >= maxHealth)
                {
                    Health = maxHealth;
                }
            }
            else Health += HealthRegenSpeed;
        }
        ApplyDamage(0);
    }
    private void LevelUp()
    {
        maxHealth = (int)(maxHealth * multiplier);
        Health = maxHealth;

        maxMana = (int)(maxMana * multiplier);
        mana = maxMana;






        RequireExp = (int)(RequireExp * (multiplier * 1.2));
        CurrentExp = 0;
        level += 1;
        damage = (int)(BaseDamage * level);
        if (level >= 5)
        {

            HealthRegenSpeed = ((int)(level / 5) + 1) * BaseHealthRegen;
            ManaRegen = ((int)(level / 5) + 1) * BaseManaRegen;
        }
        else
        {
            print("no");
            HealthRegenSpeed = BaseHealthRegen;
            ManaRegen = BaseManaRegen;
        }
        SaveGame.Save();
        //  Instantiate(LevelUpEffect, transform.position, Quaternion.identity);
        playerUi.DisplayHealth(Health, maxHealth);
        playerUi.DisplayExp(CurrentExp, RequireExp, level);

    }

    public void AddExp(int exp)
    {
        CurrentExp += exp;
        if (CurrentExp >= RequireExp)
        {
            LevelUp();
        }
        else playerUi.DisplayExp(CurrentExp, RequireExp, level);

    }
    void Respawn()
    {
        transform.position = PlayerRespawn.position * 1f;
        Health = maxHealth;
        Isdead = false;
        anim.SetIsDead(false);
        playermovement.CanMove = true;
        playerUi.DisplayHealth(Health, maxHealth);
    }
    private void PlayerDead()
    {
        anim.SetIsDead(true);
        Isdead = true;
        playermovement.CanMove = false;
        Invoke("Respawn", 2f);
    }

    public void ApplyDamage(int damage)
    {
        if (Isdead)
        {
            return;
        }
        if (damage > 0)
        {
            ///ANIMATION IS BUG, NOT FIXED YET anim.SetGetHit();
            if (IsDefend) damage = damage / 2;
            Health -= damage;
            // rb.AddForce(-transform.forward * 7f, ForceMode.VelocityChange);
            if (Health <= 0)
            {
                PlayerDead();
                Health = 0;
                playerUi.DisplayHealth(Health, maxHealth);
                //  anim.SetDead();
            }
            else playerUi.DisplayHealth(Health, maxHealth);

        }
        else if (damage == 0)
        {

            playerUi.DisplayHealth(Health, maxHealth);
        }

    }
}
