using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class BlackSmith : MonoBehaviour
{
    private string NPCname = "BlackSmith";
    public RectTransform touchArea;
    private Vector3 BaseOffset;
    public Vector3 Offset = new Vector3(0, 7, -2);
    [SerializeField] private followplayer MainCamera;
    [SerializeField] private RightTouchInputForNPC input;
    [SerializeField] private TextMeshProUGUI DialogueDisplay;
    private Coroutine CurrentCoroutine;
    private bool AllowGenDialogue = true;
    private bool AllowPress = true;
    private float timer = 0;
    private float max = 1f;
    public bool IsGiveQuest;
    private float TextGenSpeed = 0.02f;

    [SerializeField] private GameObject MainQuest1;
    [SerializeField] private GameObject MainQuest2;


    [SerializeField] private List<List<string>> Dialogue = new List<List<string>>();
    private int CurrentDialogue = 0;
    private int count = 0;

    void Awake()
    {
        string Dialogue1Quest1 = "Ah,thank you for helping me to kill those bandits, they have been stealing my iron ore for a long time";
        string Dialogue2Quest1 = " Here, as promised. I will give you a better sword and some gold";
        Dialogue.Add(new List<string> { Dialogue1Quest1 + Dialogue2Quest1 });

        string Dialogue1Quest2 = "So i another Quest for you, Im now having shortage of iron ores";
        string Dialogue2Quest2 = " Can you gather me some ore, in the Moonstone Mountain on the north of this Casttle";

        Dialogue.Add(new List<string> { Dialogue1Quest2 + Dialogue2Quest2, });
        BaseOffset = MainCamera.offset;


    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ResetAllowPress();
    }

    void TouchInput()
    {
        if (input.Pressed && AllowPress)
        {
            count++;
            AllowGenDialogue = true;
            input.Allow = false;
            input.Pressed = false;
            AllowPress = false;
        }
    }

    void ResetAllowPress()
    {
        if (AllowPress == false)
        {
            timer += Time.deltaTime;
            if (timer >= max)
            {
                AllowPress = true;
                timer = 0;
            }
        }
    }
    IEnumerator RevealText(string text)
    {


        for (int i = 0; i < text.Length; i++)
        {
            DialogueDisplay.text += text[i];
            yield return new WaitForSeconds(TextGenSpeed);
        }


    }

    void OnTriggerStay(Collider target)
    {
        if (target.CompareTag(Tags.PLAYER_TAG) && !IsGiveQuest)
        {
            MainCamera.offset = Offset;
            if (target.GetComponent<PlayerProperties>().QuestList2.Count > 0)
            {
                if (target.GetComponent<PlayerProperties>().QuestList2[0].GetGiverName() == NPCname)
                {
                    if (target.GetComponent<PlayerProperties>().QuestList2[0].IsDone == true)
                    {
                        if (AllowGenDialogue)
                        {
                            AllowGenDialogue = false;
                            CurrentCoroutine = StartCoroutine(RevealText(Dialogue[0][0]));
                            target.GetComponent<PlayerProperties>().QuestList2.RemoveAt(0);
                            foreach (GameObject temp in MainQuest1.GetComponent<Quest>().GetItemReward())
                            {
                                target.GetComponent<PlayerInventory>().AddItem(temp, 1);
                            }



                            target.GetComponent<PlayerProperties>().PublicSaveGame();

                            CurrentDialogue++;
                            target.GetComponent<PlayerProperties>().DisplayActiveQuest();
                        }
                    }
                }
            }


        }
    }
    void OnTriggerExit(Collider target)
    {
        if (target.CompareTag(Tags.PLAYER_TAG))
        {
            MainCamera.offset = BaseOffset;

        }
    }
}

