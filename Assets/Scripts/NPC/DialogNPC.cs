using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNPC : BoundedNPC
{
    // Reference to immediate dialog value
    [SerializeField] public TextAssetValue dialogValue;

    // Reference to NPC's dialog
    [SerializeField] public TextAsset myDialog;

    // Notification to sned to canvas to activate and check dialog
    [SerializeField] public Notification branchingDialogNotification;


    // Update is called once per frame
    public override void Update()
    {
        if (!playerInRange)
        {
            Move();
        }
        else
        {
            if(Input.GetButtonDown("Check"))
            {
               dialogValue.initialValue = myDialog;
               branchingDialogNotification.Raise();
               player.GetComponent<PlayerMovement>().currentState = PlayerState.interact;
            }
        }
    }
}
