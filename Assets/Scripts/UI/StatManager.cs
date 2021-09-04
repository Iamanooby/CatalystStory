using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    [SerializeField]
    public PlayerStats playerStats;

    public Text Level;
    public Text Int;
    public Text Opt;
    public Text Pot;
    public Text Ref;
    public Text Res;


    public void Start()
    {
        updateStats();
    }

    public void changeInitiative(int i)
    {
        playerStats.playerInitiative.initialValue += i;
        updateStats();
    }
    public void changeOptimism(int i)
    {
        playerStats.playerOptimism.initialValue += i;
        updateStats();
    }
    public void changePotential(int i)
    {
        playerStats.playerPotential.initialValue += i;
        updateStats();
    }
    public void changeReflective(int i)
    {
        playerStats.playerReflective.initialValue += i;
        updateStats();
    }
    public void changeResilience(int i)
    {
        playerStats.playerResilience.initialValue += i;
        updateStats();
    }


    public void updateStats()
    {
        //Debug.Log(playerStats.playerInitiative.initialValue);
        //Debug.Log(playerStats.playerOptimism.initialValue);
        //Debug.Log(playerStats.playerPotential.initialValue);
        //Debug.Log(playerStats.playerReflective.initialValue);
        //Debug.Log(playerStats.playerResilience.initialValue);
        Level.text = "LEVEL " + playerStats.playerLevel.initialValue;
        Int.text = "INITIATIVE: " + playerStats.playerInitiative.initialValue;
        Opt.text = "OPTIMISM: " + playerStats.playerOptimism.initialValue;
        Pot.text = "POTENTIAL: " + playerStats.playerPotential.initialValue;
        Ref.text = "REFLECTIVE: " + playerStats.playerReflective.initialValue;
        Res.text = "RESILIENCE: " + playerStats.playerResilience.initialValue;
    }
}
