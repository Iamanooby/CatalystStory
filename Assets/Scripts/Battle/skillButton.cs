using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillButton : MonoBehaviour
{
    // skillAssigned, need not be assigned in hierarchy
    // battleSystem, need to be assigned the battleSystem GameObject for the scene
    [HideInInspector]
    public Skills skillAssigned;
    public BattleSystem battleSystem;

    public void performAttack()
    {
        // Check if it is player's turn
        if (battleSystem.state != BattleState.PlayerTurn)
        {
            return;
        }

        // Check if player have enough PP to perform this action
        if (skillAssigned.PPConsumption <= battleSystem.player.playerPP.initialValue)
        {
            StartCoroutine(battleSystem.PlayerAttack(skillAssigned));
        }
        else
        {
            StartCoroutine(battleSystem.notEnoughPP());
        }

    }
}
