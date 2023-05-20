using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingAura : MonoBehaviour
{
    // Start is called before the first frame update
    private float counter = 1f;
    private float timer = 0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay(Collider Target)
    {
        if (Target.CompareTag(Tags.PLAYER_TAG))
        {
            // Target.gameObject.GetComponent<PlayerProperties>().RegenHealth();
            DelayHeal(Target.gameObject.GetComponent<PlayerProperties>());
        }
    }
    void DelayHeal(PlayerProperties player)
    {
        timer += Time.deltaTime;
        if (timer >= counter)
        {
            timer = 0f;
            player.RegenHealth(true);
            player.AddMana(10);
        }
    }
}
