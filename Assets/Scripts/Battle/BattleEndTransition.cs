using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleEndTransition : MonoBehaviour
{
    public string sceneToLoad;

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

    public void transitionToWorld()
    {
        // Pass Music to play
        if (needAudio)
        {
            BGMvalue.initialValue = BGM;
        }

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
