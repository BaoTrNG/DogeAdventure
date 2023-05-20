using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerQuestUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject QuestUI;
    [Multiline] public string QuestDescription;
    [Multiline] public string FullQuestDescription;
    public bool Pressed;
    private Vector3 BasePostion;
    private Vector3 BaseScale;


    public Vector3 NewScale;
    public Vector3 NewPosition;

    private RectTransform RectTransform;
    public TextMeshProUGUI QuestText;


    private int FontSize = 12;



    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        BasePostion = RectTransform.localPosition;
        BaseScale = RectTransform.localScale;
        //   QuestText.text = QuestDescription;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!Pressed)
        {
            Pressed = true;
            RectTransform.localPosition = NewPosition;
            RectTransform.localScale = NewScale;
            QuestText.text = FullQuestDescription;

        }
        else
        {
            Pressed = false;
            RectTransform.localPosition = BasePostion;
            RectTransform.localScale = BaseScale;
            QuestText.text = QuestDescription;
        }
    }
}
