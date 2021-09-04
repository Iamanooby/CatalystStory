using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
}


public class BattleSystem : MonoBehaviour
{
    // enemyStatsValue is thrown over to enemy to know which enemy we are fighting
    public EnemyStatsValue enemyStatsValue;
    public EnemyStats enemy; // <- use this to grab stats
    public FloatValue enemyCurrentHP;
    public FloatValue playerDefense;

    // To edit battle interface UI
    public Text dialogueText;
    public enemyBattleHUD enemyHUD;
    public playerBattleHUD playerHUD;

    // Import player stats
    public PlayerStats player;

    // Battle System Battle Stations
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    // Prefabs
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    // To change BattleState
    public BattleState state;

    // Player skillset
    public PlayerSkillSet playerSkills;

    // Skill Buttons
    public List<Button> skillButtons = new List<Button>();

    // Skill Desc UI
    public List<Text> skillDescUI = new List<Text>();

    // To toggle buttons
    // SkillButton, ItemButton, RunButton should take up first 3 index (0-2)
    public List<Button> UIbuttons = new List<Button>();

    // To obtain enemy gameObject and modify (whether its dead)
    public IsDead isDeadStorage;

    // For end battle transition
    public GameObject sceneTransition;

    // Start is called before the first frame update
    private void Start()
    {
        enemy = enemyStatsValue.initialValue;
        enemyPrefab = enemy.unitPrefab;
        enemyCurrentHP.initialValue = enemy.maxHP.initialValue;

        // For now, give player max PP every battle
        player.playerPP.initialValue = player.playerMaxPP.initialValue;

        hideButtons();

        // Assign skill to skillButton
        for (int i = skillButtons.Count - 1; i >= 0; i--)
        {
            // Edit skillButton UI text, and assign skill to button
            skillButtons[i].GetComponentInChildren<Text>().text = playerSkills.skillSet[i].skillName;
            skillButtons[i].GetComponent<skillButton>().skillAssigned = playerSkills.skillSet[i];
            skillDescUI[i].GetComponent<Text>().text = playerSkills.skillSet[i].skillDesc;
        }
        state = BattleState.Start;
        StartCoroutine(SetupBattle());
    }


    public IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);

        // Change dialogue text
        dialogueText.text = enemy.StartText;


        // Handle HUD for player and enemy
        playerHUD.SetHUD(player);
        enemyHUD.SetHUD(enemy, enemyCurrentHP);

        yield return new WaitForSeconds(3f);

        // Start Battle
        // Check who goes first (Checking of INITIATIVE)
        if (player.playerInitiative.initialValue >= enemy.initiative)
        {
            showButtons();
            // If player is faster
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }
        else
        {
            // If enemy is faster
            state = BattleState.EnemyTurn;
            toggleButtons();
            StartCoroutine(EnemyTurn());
        }

    }


    public IEnumerator PlayerAttack(Skills skillUsed)
    {
        // Get skill used (Done with UI Buttons)
        // Get stats of skills

        // DO UI edits
        // Disable UI buttons
        hideButtons();
        toggleSkillDesc();
        dialogueText.gameObject.SetActive(true);


        // To form player combat dialogue text
        string playerBattleText = player.playerName + " uses " + skillUsed.skillName + "\n";

        // Calculate Damage based on skill
        if (skillUsed.Attack)
        {
            float damageDealt = calculatePlayerDamage(skillUsed);
            enemyCurrentHP.initialValue -= damageDealt;
            playerBattleText += string.Format("{0:0.#} dmg...\n", damageDealt);
        }

        // Calculate defense based on skill
        if (skillUsed.Defense)
        {
            playerDefense.initialValue = skillUsed.defenseValue;
            playerBattleText += string.Format("{0:0.#} def...\n", playerDefense.initialValue);
        }

        // Calculate heal based on skill
        if (skillUsed.heal)
        {
            float healAmt = calculateHealingValue(skillUsed);
            player.playerCurrentHP.initialValue += healAmt;

            // If overheal
            if (player.playerCurrentHP.initialValue > player.playerMaxHP.initialValue)
            {
                player.playerCurrentHP.initialValue = player.playerMaxHP.initialValue;
            }

            playerBattleText += string.Format("{0:0.#} hp recovered...\n", healAmt);
        }

        // Calculate how much PP was used
        player.playerPP.initialValue -= skillUsed.PPConsumption;

        // Edit enemy Health UI
        enemyHUD.SetHP(enemyCurrentHP);
        playerHUD.SetHP(player.playerCurrentHP);

        // Edit player PP UI
        playerHUD.SetPP(player.playerPP);

        dialogueText.text = playerBattleText;

        // Wait for X seconds to show text
        yield return new WaitForSeconds(3f);

        // Reset UI
        startingUISetup();


        // Check if the enemy is dead
        bool enemyIsDead;

        if (enemyCurrentHP.initialValue <= 0)
        {
            enemyIsDead = true;

            // Set enemy gameobject isDead bool to TRUE
            isDeadStorage.isDead = true;
        }
        else
        {
            enemyIsDead = false;
        }

        // What shall we do next (change state based on what happened)
        if (enemyIsDead)
        {
            state = BattleState.Won;
            EndBattle();
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }


    public void EndBattle()
    {
        StartCoroutine(playerEndBattleUI());
    }


    public void PlayerTurn()
    {
        // Activate buttons
        showButtons();

        // Toggle interactable UI buttons
        toggleButtons();

        // Show dialogue text
        dialogueText.text = "What shall I do?";

    }


    public IEnumerator EnemyTurn()
    {
        // Toggle interactable UI buttons
        toggleButtons();

        // Indicate enemy turn
        dialogueText.text = enemy.name + "'s turn...";
        yield return new WaitForSeconds(2f);

        // Player Take Damage
        float damage = calculateEnemyDamage();
        player.playerCurrentHP.initialValue -= damage;

        // Tell player how many damage was dealt
        dialogueText.text = player.playerName + " takes " + damage + " dmg...";
        yield return new WaitForSeconds(2f);

        // Check if player is Dead
        bool playerIsDead;
        if (player.playerCurrentHP.initialValue <= 0)
        {
            playerIsDead = true;
            // Set player base health to 0 when dead
            player.playerCurrentHP.initialValue = 0;
        }
        else
        {
            playerIsDead = false;
        }

        // Update player health after enemy attacks
        playerHUD.SetHP(player.playerCurrentHP);
        enemyHUD.SetHP(enemyCurrentHP);


        // What to do next
        if (playerIsDead)
        {
            state = BattleState.Lost;
            EndBattle();
        }
        else
        {
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }
    }

    public void runAway()
    {
        // Funct: If player chooses "Run" button

        hideButtons();
        dialogueText.text = "You escaped...";

        // Exit battle scene
        sceneTransition.GetComponent<BattleEndTransition>().transitionToWorld();
    }


    // FOLLOWING IS A LIST OF SCRIPTS TO CALCULATE DAMAGE, HEAL, DEF ETC.
    public float calculateEnemyDamage()
    {
        float damage;
        damage = Mathf.Round(enemy.damage - (playerDefense.initialValue * 0.2f));
        return damage;
    }


    public float calculatePlayerDamage(Skills skillUsed)
    {
        float damageDealt;
        damageDealt = Mathf.Round(skillUsed.skillDamage * (player.playerOptimism.initialValue * 1.1f));
        return damageDealt;
    }


    public float calculateHealingValue(Skills skillUsed)
    {
        float healAmt;
        healAmt = Mathf.Round(skillUsed.healValue * (player.playerReflective.initialValue * 1.1f));
        return healAmt;
    }


    // FOLLOWING IS A LIST OF SCRIPTS TO HANDLE MISC ACTIONS
    public IEnumerator notEnoughPP()
    {
        hideButtons();
        toggleSkillDesc();
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "Not enough Potential Power to perform this action...";
        yield return new WaitForSeconds(3f);
        dialogueText.gameObject.SetActive(false);
        showButtons();
        toggleSkillDesc();
    }




    // FOLLOWING IS A LIST OF UI SCRIPT
    public void toggleButtons()
    {
        // Funct: To toggle buttons whether its interactable between dialogues
        for (int i = UIbuttons.Count - 1; i >= 0; i--)
        {
            UIbuttons[i].interactable = !UIbuttons[i].interactable;
        }
    }


    public void hideButtons()
    {
        // Funct: To disable buttons
        for (int i = UIbuttons.Count - 1; i >= 0; i--)
        {
            UIbuttons[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        }
    }


    public void showButtons()
    {
        // Funct: To enable buttons
        for (int i = UIbuttons.Count - 1; i >= 0; i--)
        {
            UIbuttons[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }

    public void toggleSkillDesc()
    {
        for (int i = skillDescUI.Count - 1; i >= 0; i--)
        {
            skillDescUI[i].gameObject.SetActive(!skillDescUI[i].IsActive());
        }
    }

    public void startingUISetup()
    {
        for (int i = UIbuttons.Count - 1; i >= 3; i--)
        {
            UIbuttons[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            UIbuttons[i].gameObject.SetActive(true);
        }
    }

    void levelUp()
    {
        // Increase player lvl
        player.playerLevel.initialValue++;

        // change player's required EXP to level
        player.playerEXPToLevel.initialValue += 5;

        // Increase Player stats
        player.playerMaxHP.initialValue += 2;
        player.playerCurrentHP.initialValue = player.playerMaxHP.initialValue;
        
        player.playerMaxPP.initialValue += 5;
        player.playerPP.initialValue = player.playerMaxPP.initialValue;

        player.playerOptimism.initialValue += 1;
        player.playerPotential.initialValue += 1;
        player.playerReflective.initialValue += 1;
        player.playerResilience.initialValue += 1;
        player.playerInitiative.initialValue += 1;

    }

    public IEnumerator playerEndBattleUI()
    {
        if (state == BattleState.Won)
        {
            // Enemy victory text...
            dialogueText.text = enemy.victoryText;
            yield return new WaitForSeconds(2f);
            dialogueText.text = string.Format("{0} gained {1:0.#} exp!", player.playerName, enemy.exp.initialValue);
            yield return new WaitForSeconds(2f);

            // Add exp to player
            player.playerEXP.initialValue += enemy.exp.initialValue;

            if (player.playerEXP.initialValue >= player.playerEXPToLevel.initialValue)
            {

                // If player exp higher than playerexp to level
                // THIS SHOW BE IN SOMEWHERE BETTER IN THE FUTURE
                while (player.playerEXP.initialValue >= player.playerEXPToLevel.initialValue)
                {
                    // PLAYER LEVEL UP!!!
                    // Find out if got excess exp
                    float leftover = player.playerEXP.initialValue - player.playerEXP.initialValue;

                    // add leftover exp to player
                    player.playerEXP.initialValue = leftover;

                    levelUp();
                }

                // Show dialogue text (CONGRATULATE)
                dialogueText.text = string.Format("{0} has level up! Lvl: {1}", player.playerName, player.playerLevel.initialValue);
                yield return new WaitForSeconds(3f);
            }


        }
        else if (state == BattleState.Lost)
        {
            // Enemy defeat text...
            dialogueText.text = enemy.defeatText;            
            yield return new WaitForSeconds(5f);

            // Give player a chance
            player.playerCurrentHP.initialValue = player.playerMaxHP.initialValue;
        }

        // Exit battle scene
        sceneTransition.GetComponent<BattleEndTransition>().transitionToWorld();
    }
}