using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStats : MonoBehaviour
{
    public EnemyStats enemy;
    public float currentHP;

    private void Start()
    {
        currentHP = enemy.maxHP.initialValue;
    }

}
