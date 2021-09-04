using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    public GameObject enemyObject;

    // Start is called before the first frame update
    void Start()
    {
        enemyObject = this.transform.parent.gameObject; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // When player hits an enemy GameObject (vice versa)
            StartCoroutine(this.GetComponentInParent<BattleInterface>().battle());
        }
    }
}
