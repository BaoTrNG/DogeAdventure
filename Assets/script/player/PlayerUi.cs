using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    private Image HealthBar;
    private TextMeshProUGUI HealthText;

    private Image ManaBar;
    private TextMeshProUGUI ManaText;

    private Image ExpBar;
    private TextMeshProUGUI ExpTextInside;
    private TextMeshProUGUI ExpTextOutside;

    public TextMeshProUGUI PlayerInfo;
    private TextMeshProUGUI DashCoolDown;
    private float counter = 4f;
    public bool IsCooldown = true;

    private void Awake()
    {
        HealthBar = GameObject.FindWithTag(Tags.HEALTH_DISPLAY_TAG).GetComponent<Image>();
        HealthText = GameObject.FindWithTag(Tags.HEALTH_TEXT_TAG).GetComponent<TextMeshProUGUI>();

        ManaBar = GameObject.FindWithTag(Tags.MANA_DISPLAY_TAG).GetComponent<Image>();
        ManaText = GameObject.FindWithTag(Tags.MANA_TEXT_TAG).GetComponent<TextMeshProUGUI>();



        ExpBar = GameObject.FindWithTag(Tags.PLAYER_EXP_DISPLAY).GetComponent<Image>();
        ExpTextInside = GameObject.FindWithTag(Tags.PLAYER_EXP_TEXT).GetComponent<TextMeshProUGUI>();
        ExpTextOutside = GameObject.FindWithTag(Tags.PLAYER_EXP_TEXT_DISPLAY).GetComponent<TextMeshProUGUI>();

        PlayerInfo = GameObject.FindWithTag(Tags.PLAYER_INFO_TEXT).GetComponent<TextMeshProUGUI>();
        DashCoolDown = GameObject.FindWithTag(Tags.PLAYER_DASHBTN_TEXT_COOLDOWN).GetComponent<TextMeshProUGUI>();

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsCooldown)
        {
            DisplayDashCooldown();
        }
    }
    public void DisplayDashCooldown()
    {
        DashCoolDown.enabled = true;
        counter -= Time.deltaTime;
        int display = (int)counter;
        DashCoolDown.text = display.ToString();
        if (counter <= 1)
        {
            DashCoolDown.enabled = false;
            IsCooldown = true;
            counter = 4f;

        }

    }
    public void DisplayExp(int Exp, int MaxExp, int level)
    {
        float value = (float)Exp / MaxExp;
        ExpBar.fillAmount = value;
        string TextInside = Exp + " / " + MaxExp;
        string TextOutside = "Lv." + level;

        ExpTextInside.text = TextInside;
        ExpTextOutside.text = TextOutside;
    }
    public void DisplayHealth(float PlayerHealth, float maxhealth)
    {
        float value = PlayerHealth / maxhealth;
        string HealthStr = PlayerHealth + " / " + maxhealth;
        if (value < 0) value = 0;

        HealthBar.fillAmount = value;
        HealthText.text = HealthStr;
    }

    public void DisplayMana(float PlayerMana, float maxmana)
    {
        float value = PlayerMana / maxmana;
        string ManaStr = PlayerMana + " / " + maxmana;
        if (value <= 0) value = 0f;

        ManaBar.fillAmount = value;
        ManaText.text = ManaStr;
    }

}
