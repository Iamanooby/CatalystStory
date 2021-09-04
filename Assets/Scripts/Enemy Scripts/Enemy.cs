using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack
}


public class Enemy : MonoBehaviour
{
    // Script: Coding for any enemy object
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;

    public Transform target;

    public Rigidbody2D myRigidbody;
    public Animator anim;

    public bool isDead;

    public void Awake()
    {
        health = maxHealth.initialValue;
    }

    public void Update()
    {
        if (isDead)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
