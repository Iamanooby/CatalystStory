using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBattleHUD : enemyBattleHUD
{
    public Text ppText;
    public Slider ppSlider;

    public virtual void SetHUD(PlayerStats player)
    {
        nameText.text = player.playerName;
        levelText.text = "Lvl " + player.playerLevel.initialValue;

        hpText.text = player.playerCurrentHP.initialValue.ToString();
        hpSlider.maxValue = player.playerMaxHP.initialValue;
        hpSlider.value = player.playerCurrentHP.initialValue;

        ppSlider.maxValue = player.playerMaxPP.initialValue;
        ppSlider.value = player.playerPP.initialValue;
        ppText.text = player.playerPP.initialValue.ToString();
    }

    public void SetPP(IntValue CurrentPP)
    {
        // To set HP when enemy takes damage
        ppSlider.value = Mathf.Round(CurrentPP.initialValue);

        if (CurrentPP.initialValue >= 0)
        {
            ppText.text = string.Format("{0:0.#}", CurrentPP.initialValue);
        }
        else
            ppText.text = "0";

    }
}