using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogNPC : DialogNPC
{
    // Reference to NPC's dialog
    [SerializeField] private TextAsset keyDialog;

    // Get the player inventory to check for key
    [SerializeField] private Inventory playerInventory;

    public override void Update()
    {
        if (!playerInRange)
        {
            Move();
        }
        else
        {
            if (Input.GetButtonDown("Check"))
            {
                if (playerInventory.numberOfKeys < 1)
                {
                    // If player haven found the key
                    dialogValue.initialValue = myDialog;
                }
                else
                {
                    // If player found the key
                    dialogValue.initialValue = keyDialog;
                }
                branchingDialogNotification.Raise();
                player.GetComponent<PlayerMovement>().currentState = PlayerState.interact;
            }
        }
    }
}
