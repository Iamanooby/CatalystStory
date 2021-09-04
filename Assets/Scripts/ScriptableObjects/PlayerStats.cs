using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    // Define player stats
    public FloatValue playerEXP;
    public FloatValue playerEXPToLevel;
    public FloatValue playerCurrentHP;
    public FloatValue playerMaxHP;
    public Inventory playerInventory;
    public IntValue playerLevel;
    public string playerName;
    public IntValue playerMaxPP;
    public IntValue playerPP;

    // Player skills
    public PlayerSkillSet playerSkillSet;

    // Player Ability Stats
    public IntValue playerInitiative;
    public IntValue playerOptimism;
    public IntValue playerPotential;
    public IntValue playerReflective;
    public IntValue playerResilience;
    
}
