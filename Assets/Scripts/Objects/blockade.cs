using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BlockadeType
{
    key,
    enemy,
    button
}

public class blockade : Interactable
{
    [Header("Blockade Variables")]
    public BlockadeType thisBlockadeType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer blockadeSprite;
    

    // For blockade sprite collider
    public BoxCollider2D physicsCollider;


    // blockadeCleared storage to know help with loading scenes
    public BoolValue blockadeCleared;


    public GameObject dialogBox;
    public Text dialogText;

    // Dialog when not enough keys
    public string dialogNoKeys;
    // Dialog when blockade cleared
    public string dialogCleared;
    public GameObject player;

    public void Start()
    {
        if (blockadeCleared.initialValue)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        if (Input.GetButtonDown("Check"))
        {
            if (playerInRange && thisBlockadeType == BlockadeType.key)
            {
                // Does the player have a key?
                if (playerInventory.numberOfKeys > 0)
                {
                    // Clear blockade if player has keys
                    playerInventory.numberOfKeys--;
                    raiseDialogueText(dialogCleared);
                    Open();
                }
                else
                {
                    // Stop Player
                    raiseDialogueText(dialogNoKeys);
                    if (open == true)
                    {
                        // Switch off triggerCollider if door was opened and player just closing dialog
                        this.GetComponent<BoxCollider2D>().enabled = false;
                        blockadeCleared.initialValue = true;
                    }
                }


                // Yes, call open method
            }
        }
    }

    private void raiseDialogueText(string dialogToShow)
    {
        // Stop player
        if (dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
            player.GetComponent<PlayerMovement>().currentState = PlayerState.idle;
        }
        else
        {
            dialogBox.SetActive(true);
            dialogText.text = dialogToShow;
            player.GetComponent<PlayerMovement>().currentState = PlayerState.interact;
        }
    }


    public void Open()
    {
        // Turn blockade sprite renderer off
        blockadeSprite.enabled = false;
        // set open to true
        open = true;
        // turn off blockade box collider off
        physicsCollider.enabled = false;
        // turn off trigger area
        player.GetComponent<PlayerMovement>().currentState = PlayerState.idle;
    }

    public void Close()
    {

    }
}
