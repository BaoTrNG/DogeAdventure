using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guider : MonoBehaviour
{
    public RectTransform touchArea;
    private Vector3 BaseOffset;
    public Vector3 Offset = new Vector3(0, 7, -2);
    [SerializeField] private followplayer MainCamera;
    [SerializeField] private RightTouchInputForNPC input;
    [SerializeField] private TextMeshProUGUI DialogueDisplay;
    private Coroutine CurrentCoroutine;
    private List<string> dialogue = new List<string>();
    private bool AllowGenDialogue = true;
    private bool AllowPress = true;
    private float timer = 0;
    private float max = 1f;
    public bool IsGiveQuest;
    [SerializeField] private GameObject MainQuest1;
    [SerializeField] private PlayerProperties Player;

    private int count = 0;
    public float TextGenSpeed;
    void Awake()
    {
        BaseOffset = MainCamera.offset;
        string text1 = "Hello,I assume that you are new to this game,so I will guide you to the first quest (touch right screen to continue)";
        string text2 = "If you look to your left, you can see a portal. Get in and help me to kill 2 \"bandit\"  (press again to accept quest)";

        dialogue.Add(text1);
        dialogue.Add(text2);

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ResetAllowPress();
        //  print(dialogue.Count);
        //  print(count);
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
            if (AllowGenDialogue)
            {
                AllowPress = false;
                AllowGenDialogue = false;
                if (count >= dialogue.Count)
                {
                    print("yes");
                    count = dialogue.Count - 1;
                    if (!IsGiveQuest)
                    {
                        IsGiveQuest = true;
                        target.GetComponent<PlayerProperties>().AddMainQuest(MainQuest1);
                    }
                }
                if (CurrentCoroutine != null)
                {
                    StopCoroutine(CurrentCoroutine);
                    DialogueDisplay.text = "";
                }
                CurrentCoroutine = StartCoroutine(RevealText(dialogue[count]));
            }
            TouchInput();
        }
    }
    void OnTriggerExit(Collider target)
    {
        if (target.CompareTag(Tags.PLAYER_TAG))
        {

            // StopCoroutine(RevealText(dialogue[count]));
            if (CurrentCoroutine != null)
            {
                StopCoroutine(CurrentCoroutine);
            }
            //  StopCoroutine(CurrentCoroutine);
            DialogueDisplay.text = "";
            MainCamera.offset = BaseOffset;
            DialogueDisplay.text = "";
            AllowGenDialogue = true;
            count = 0;


        }
    }
}
