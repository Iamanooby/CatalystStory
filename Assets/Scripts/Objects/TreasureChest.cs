using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Signal raiseItem;

    // To show desc of item
    public GameObject dialogBox;
    public Text dialogText;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!isOpen)
            {
                // Open chest
                OpenChest();
            }
            else
            {
                // Chest is alrdy open
                ChestOpened();
            }
        }
    }

    public void OpenChest()
    {
        // Dialog Window on
        dialogBox.SetActive(true);

        // Dialog text = contents text
        dialogText.text = contents.itemDescription;

        // add contents to inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;

        // Raise the signal to the player to animate 
        raiseItem.Raise();

        // raise the context clue
        context.Raise();

        // set the chest to opened
        isOpen = true;

        // Chest open animation
        anim.SetBool("opened", true);
    }

    public void ChestOpened()
    {
        // Dialog Off
        dialogBox.SetActive(false);

        // raise the signal to the player to stop animating
        raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}
