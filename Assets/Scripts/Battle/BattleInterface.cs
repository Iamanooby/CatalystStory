using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleInterface : MonoBehaviour
{
    public string sceneToLoad;

    public EnemyStats enemystats;
    public EnemyStatsValue savedStats;
    public IsDead isDeadStorage;
    public GameObject currentEnemyGameObject;

    // For player position
    public GameObject playerPosition;
    public Vector2 playerPositionVector;
    public VectorValue playerStorage;

    // For scene audio
    public bool needAudio;
    public AudioClip BGM;
    public AudioClipValue BGMvalue;

    // For Fading Transition
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    // For Camera Min, Max Position
    public Vector2 cameraMaxPosition;
    public Vector2 cameraMinPosition;
    public VectorValue camMaxPos;
    public VectorValue camMinPos;


    public virtual void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;

            // Destroy panel after 1 sec
            Destroy(panel, 1);
        }
    }

    public IEnumerator battle()
    {
        // Function for when enemy is hit
        // Remember player's current position
        //playerPositionVector = (Vector2)playerPosition.transform.position;
        //playerStorage.initialValue = playerPositionVector;


        // Save cam max/min pos
        // camMaxPos.initialValue = cameraMaxPosition;
        // camMinPos.initialValue = cameraMinPosition;

        // Pass enemystats to savedStats for use in battle scene
        savedStats.initialValue = enemystats;

        // Pass GameObject value
        isDeadStorage.enemyName = currentEnemyGameObject.name;
        isDeadStorage.isDead = currentEnemyGameObject.GetComponent<Enemy>().isDead;

        // Change scenes
        if (needAudio)
        {
            BGMvalue.initialValue = BGM;
        }
        yield return null;
        StartCoroutine(FadeCo());
    }

    public IEnumerator FadeCo()
    {

        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);


        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
