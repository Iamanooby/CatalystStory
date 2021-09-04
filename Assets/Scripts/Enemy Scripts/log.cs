using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log : Enemy
{
    // Inherit from "Enemy" class

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // Target (Player GameObject)
        target = GameObject.FindWithTag("Player").transform;
        anim.SetBool("wakeUp", true);
    }

    // FixedUpdate, updates at frequency of physics engine
    void FixedUpdate()
    {
        CheckDistance();
    }

    // Virtual so that can be overriden in PatrolLog class GameObject
    public virtual void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            // Check Enemy State is 'Idle' or 'Walk'
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                // Move enemy GameObject towards Player
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                // Get actual distance moved
                changeAnim(temp - transform.position);

                // Move GameObject to calculated 'temp'
                myRigidbody.MovePosition(temp);

                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("wakeUp", false);
        }
    }

    private void ChangeState(EnemyState newState)
    {
        // To change enemy state
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        // Script: Used to set vector to the 'moveX' and 'moveY' parameter
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    public void changeAnim(Vector2 direction)
    {
        // Script: Used to change 'log' object animation
        // Animator Blend tree requires moveX (1 || -1) moveY (1 || -1)

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                // Direction: RIGHT
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                // Direction: LEFT
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                // Direction: UP
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                // Direction: DOWN
                SetAnimFloat(Vector2.down);
            }
        }
    }


}
