using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    public string unitName;
    public int unitLevel;
    public GameObject unitPrefab;

    public float damage;

    public FloatValue maxHP;

    public FloatValue exp;

    public int initiative;

    // For battle Text
    public string victoryText;
    public string defeatText;
    public string StartText;
    
}
