using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    // Script: Used to change dialog box in the game UI

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public GameObject player;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Check") && playerInRange)
        {
            if(dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                player.GetComponent<PlayerMovement>().currentState = PlayerState.idle;
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
                player.GetComponent<PlayerMovement>().currentState = PlayerState.interact;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            context.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }

}
