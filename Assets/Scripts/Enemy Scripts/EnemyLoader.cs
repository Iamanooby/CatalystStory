using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoader : MonoBehaviour
{
    // Class: EnemyLoader is used to update the enemies everything the scene is loaded (i.e. after a battle)

    public IsDead isDeadStorage;
    public List<GameObject> EnemyGameObject = new List<GameObject>();

    public void Start()
    {
        for (int i = EnemyGameObject.Count - 1; i >= 0; i--)
        {
            if (EnemyGameObject[i].name == isDeadStorage.enemyName)
            {
                EnemyGameObject[i].GetComponent<Enemy>().isDead = isDeadStorage.isDead;
            }
        }

        // Destroy storage gameobject
        isDeadStorage.isDead = false;
        isDeadStorage.name = null;

    }
}
