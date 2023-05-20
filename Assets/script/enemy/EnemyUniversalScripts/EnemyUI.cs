using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Slider EnemyHealthSlider;
    [SerializeField] private TextMeshProUGUI DamageText;
    [SerializeField] private TextMeshProUGUI LevelText;

    private PlayerProperties player;
    public bool IsOrcBoss;
    private EnemyProperties enemyProperties;
    private void Awake()
    {
        //  EnemyHealthSlider = GameObject.FindWithTag(Tags.ENEMY_HEALTH_DISPLAY_TAG).GetComponent<Slider>();
        //   DamageText = GameObject.FindWithTag(Tags.ENEMY_DAMAGE_TEXT).GetComponent<TextMeshProUGUI>();
        //  LevelText = GameObject.FindWithTag(Tags.ENEMY_LV_TEXT).GetComponent<TextMeshProUGUI>();
        player = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerProperties>();
        if (IsOrcBoss) enemyProperties = GameObject.FindWithTag(Tags.ORC_BOSS_TAG).GetComponent<EnemyProperties>();
        else enemyProperties = GameObject.FindWithTag(Tags.ENEMY_TAG).GetComponent<EnemyProperties>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DisplayLv(float EnemyLevel, string EnemyName)
    {
        if (player.level + 3 < EnemyLevel)
        {
            LevelText.color = Color.red;

        }
        else if (IsOrcBoss) LevelText.color = Color.red;
        else LevelText.color = Color.white;
        string LevelStr = "Level." + EnemyLevel + ": " + EnemyName;
        LevelText.text = LevelStr;
    }

    public void DisplayHealth(float damage, float maxhealth)
    {
        //print(damage + " " + maxhealth);
        float value = damage / maxhealth;
        EnemyHealthSlider.value = value;

    }


}
