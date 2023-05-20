using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class Quest : MonoBehaviour
{
    public int QuestID;
    [SerializeField] private string GiverName;

    // [SerializeField] private string TakerName
    public bool IsDone;

    [Multiline] public string ShortDescription;
    public string AmountDoneString;
    [Multiline] public string Description;
    [SerializeField] private int ExpReward;
    [SerializeField] private int Gold;
    [SerializeField] private int QuestType; //0 for delivering, 1 for kill, 2 for gathering
    private int CurrentStep = 0;
    [SerializeField] private string TargetName;
    [SerializeField] private int TargetAmount;
    [SerializeField] private int Count = 0;

    public string GetTargetName() { return TargetName; }
    public void SetTargetName(string value) { TargetName = value; }

    public int GetTargetAmount() { return TargetAmount; }
    public void SetTargetAmount(int value) { TargetAmount = value; }

    public void AddCount() { Count++; }
    public int GetCount() { return Count; }
    public void SetCount(int value) { Count = value; }
    public int GetQuestType() { return QuestType; }
    public void SetQuestType(int value) { QuestType = value; }
    public void SetIsDone() { IsDone = true; }
    public void SetAmountDoneString() { AmountDoneString = Count + "/" + TargetAmount; }
    public string GetGiverName() { return GiverName; }
    public void SetGiverName(string value) { GiverName = value; }
    public int GetExpReward() { return ExpReward; }
    public void SetExpReward(int value) { ExpReward = value; }
    public int GetGold() { return Gold; }
    public void SetGold(int value) { Gold = value; }
    public int GetCurrentStep() { return CurrentStep; }
    public void SetCurrentStep(int value) { CurrentStep = value; }
    [SerializeField] private List<GameObject> ItemReward = new List<GameObject>();
    private List<GameObject> QuestSteps = new List<GameObject>();
    /*  void Awake()
      {
          if (QuestType == 1)
          {
              AmountDoneString = Count.ToString() + "/" + TargetAmount;
          }
      }*/

    public List<GameObject> GetItemReward() { return ItemReward; }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }




}
