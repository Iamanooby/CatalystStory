using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedNPC : Interactable
{
    private Vector3 directionVector;
    private Transform myTransform;
    public float speed;
    private Rigidbody2D myRigidbody;
    public Animator anim;
    public Collider2D boundary;
    public GameObject player;

    // Start is called before the first frame update
    public void Start()
    {
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!playerInRange)
        {
            Move();
        }
    }

    public void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.fixedDeltaTime;
        if (boundary.bounds.Contains(temp))
        {
            myRigidbody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
    }


    public void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch(direction)
        {
            case 0:
                // Walking to Right
                directionVector = Vector3.right;
                break;
            case 1:
                // Walking Up
                directionVector = Vector3.up;
                break;
            case 2:
                // Walking Left
                directionVector = Vector3.left;
                break;
            case 3:
                // Walking Down
                directionVector = Vector3.down;
                break;
            default:
                break;
        }
        UpdateAnimation();
    }

    public void UpdateAnimation()
    {
        anim.SetFloat("moveX", directionVector.x);
        anim.SetFloat("moveY", directionVector.y);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Vector3 temp = directionVector;
        ChangeDirection();
        int loops = 0;
        while(temp == directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }
}
