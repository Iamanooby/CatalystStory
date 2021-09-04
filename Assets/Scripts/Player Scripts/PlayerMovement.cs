using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    // Define player states
    walk,
    attack,
    idle,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    // @@@ TO EDIT PLAYER HEALTH @@@
    public FloatValue currentHealth;
    public Signal playerHealthSignal;

    // To Transform player to allocated position when scene transitions
    public VectorValue startingPosition;

    // To access player Inventory
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;



    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        // Get gameOBJ components
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Set state of player for hitboxes if player haven move
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        // Transform player to allocated position
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is in interaction state
        // Player should not move when interacting with an interactable object
        if (currentState == PlayerState.interact)
        {
            animator.SetBool("moving", false);
            return;
        }

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        
        // GetButtonDown("attack") is found in input Manager buttons.
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            // When player interacts with treasure chest
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receive item", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receive item", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }


    private IEnumerator AttackCo()
    {
        // Animator: Player attacks
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;

        // Wait a frame
        yield return null;
        animator.SetBool("attacking", false);

        // **animation total length is 21s = .35 of a sec**
        yield return new WaitForSeconds(.35f);

        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    void UpdateAnimationAndMove()
    {
        // If there is player movement inputs
        if (change != Vector3.zero)
        {
            MoveCharacter();

            // Show animations based on input value...
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);

            // Set "moving" bool to TRUE to indicate that player is moving
            animator.SetBool("moving", true);
        }
        else
        {
            // Set "moving" bool to FALSE to indicate player is idle (triggers animator)
            animator.SetBool("moving", false);
        }

    }

    void MoveCharacter()
    {
        // Normalise vector3 to length 1.0
        change.Normalize();
        playerHealthSignal.Raise();

        // Transform character to new position
        myRigidbody.MovePosition(transform.position + change * speed * Time.fixedDeltaTime);
    }
}
