using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skills : ScriptableObject
{
    // Skill Name and Desc
    public string skillName;
    public string skillDesc;

    // General skillDamage
    public bool Attack;
    public int skillDamage;

    // Skill Potential power consumption
    public int PPConsumption;
    
    // Indicate if skill is attacking or defending.
    public bool Defense;
    public int defenseValue;

    // Heal value
    public bool heal;
    public int healValue;
}
