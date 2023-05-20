using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
[Serializable]
public class PlayerQuest
{
    public int QuestID;
    public string GiverName;
    public bool IsDone;
    public string ShortDescription;
    public string AmountDoneString;
    public string Description;
    public int ExpReward;
    public int Gold;
    public int QuestType;
    public int CurrentStep;
    public string TargetName;
    public int TargetAmount;
    public int Count;
}

[Serializable]

public class SaveData
{
    public int maxHealth;
    public int HealthRegenSpeed;
    public int maxMana;
    public int ManaRegen;
    public int level;
    public int RequireExp;
    public int currentExp;
    public List<PlayerQuest> QuestList2 = new List<PlayerQuest>();
    public Dictionary<GameObject, int> inventory = new Dictionary<GameObject, int>();
    // public List<GameObject> QuestList = new List<GameObject>();
}

public class PlayerSaveLoad : MonoBehaviour
{
    private PlayerProperties target;
    private PlayerProperties loadplayer;
    private string path;

    void Awake()
    {

    }
    void Start()
    {
        loadplayer = GetComponent<PlayerProperties>();
        path = Application.persistentDataPath + "/SaveGame.json";
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonString);
            if (data != null)
                loadplayer.SetProperties(data.maxHealth, data.maxMana, data.HealthRegenSpeed, data.ManaRegen, data.level, data.RequireExp, data.currentExp, data.QuestList2);

        }
    }

    // Update is called once per frame
    void Update()
    {


    }
    void LoadGame()
    {
        path = Application.persistentDataPath + "/SaveGame.json";
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonString);
            loadplayer.GetComponent<PlayerProperties>().SetProperties(data.maxHealth, data.maxMana, data.HealthRegenSpeed, data.ManaRegen, data.level, data.RequireExp, data.currentExp, data.QuestList2);
        }
        else print(path);


    }
    public void Save()
    {
        target = GetComponent<PlayerProperties>();
        SaveData saveobject = new SaveData();
        saveobject.QuestList2 = target.QuestList2.Select(q => new PlayerQuest
        {
            QuestID = q.QuestID,
            GiverName = q.GetGiverName(),
            IsDone = q.IsDone,
            ShortDescription = q.ShortDescription,
            AmountDoneString = q.AmountDoneString,
            Description = q.Description,
            ExpReward = q.GetExpReward(),
            Gold = q.GetGold(),
            QuestType = q.GetQuestType(),
            CurrentStep = q.GetCurrentStep(),
            TargetName = q.GetTargetName(),
            TargetAmount = q.GetTargetAmount(),
            Count = q.GetCount()
        }).ToList();
        //  saveobject.QuestList = new List<Quest>();
        /*    foreach (GameObject temp in target.QuestList)
        {
            // print("des " + temp.GetComponent<Quest>().Description);
            saveobject.QuestList2.Add(temp.GetComponent<Quest>());
            saveobject.QuestList.Add(temp);
        }
        */
        // print(saveobject.QuestList.Count);
        // string test = JsonUtility.ToJson(saveobject.QuestList[0]);
        //  print("test");

        saveobject.maxHealth = target.GetMaxHealth();
        saveobject.HealthRegenSpeed = target.GetHealthRegenSpeed();
        saveobject.maxMana = target.GetMaxMana();
        saveobject.level = target.level;
        saveobject.currentExp = target.CurrentExp;
        saveobject.RequireExp = target.RequireExp;
        saveobject.ManaRegen = target.GetManaRegenSpeed();
        saveobject.inventory = target.GetComponent<PlayerInventory>().GetInventory();
        foreach (KeyValuePair<GameObject, int> temp in saveobject.inventory)
        {
            print(temp.Key + " x " + temp.Value);
        }

        // string json = JsonUtility.ToJson(saveobject);
        string json = JsonConvert.SerializeObject(saveobject);

        string localpath = Application.persistentDataPath + "/SaveGame.json";


        File.WriteAllText(localpath, json);

        saveobject = null;

    }
}
