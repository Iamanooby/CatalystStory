using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyBattleHUD : MonoBehaviour
{

    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    public Text hpText;

    public virtual void SetHUD(EnemyStats enemy, FloatValue enemyCurrentHP)
    {
        nameText.text = enemy.unitName;
        levelText.text = "Lvl " + enemy.unitLevel;

        // Edit hp of enemy
        hpText.text = string.Format("{0:0.#}", enemyCurrentHP.initialValue);
        hpSlider.maxValue = enemy.maxHP.initialValue;
        hpSlider.value = enemyCurrentHP.initialValue;

    }

    public void SetHP(FloatValue CurrentHP)
    {
        // To set HP when enemy takes damage
        hpSlider.value = Mathf.Round(CurrentHP.initialValue);

        if (CurrentHP.initialValue >= 0)
        {
            hpText.text = string.Format("{0:0.#}", CurrentHP.initialValue);
        }
        else
            hpText.text = "0";

    }
}
