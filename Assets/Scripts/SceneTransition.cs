using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;

    // For Player position
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    // For camera position
    public Vector2 CamMaxPosition;
    public VectorValue CamMaxPos;
    public Vector2 CamMinPosition;
    public VectorValue CamMinPos;

    // For scene audio
    public bool needAudio;
    public AudioClip BGM;
    public AudioClipValue BGMvalue;

    // For Fading Transition
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;


    public virtual void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            
            // Destroy panel after 1 sec
            Destroy(panel, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            CamMinPos.initialValue = CamMinPosition;
            playerStorage.initialValue = playerPosition;

            CamMaxPos.initialValue = CamMaxPosition;
            if (needAudio)
            {
                BGMvalue.initialValue = BGM;
            }
            StartCoroutine(FadeCo());
            //SceneManager.LoadScene(sceneToLoad);
        }
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
