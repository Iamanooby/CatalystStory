using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class PlayerMeetBoss : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public PlayableDirector timeline;
    bool FirstTime = true;
    public GameObject player;
    public StringListValue bossDialogList;
    public StringListValue creditDialogList;
    public GameObject transitToBlackPanel;
    public GameObject HUDgameObject;

    //Music for boss dialog
    public AudioSource AM;
    public AudioClip bossAudio;
    public AudioClip creditAudio;

    private void start()
    {
        dialogText.text = "...";
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) //&& FirstTime)
        {
            player.GetComponent<PlayerMovement>().currentState = PlayerState.interact;
            changeMusic(bossAudio);
            timeline.Play();
            HUDgameObject.SetActive(false);

        }
    }

    private void changeMusic(AudioClip clipToChange)
    {
        // Play boss music
        AM.Stop();
        AM.clip = clipToChange;
        AM.Play();
    }

    private void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (timeline == aDirector)
        {
            Debug.Log("PlayableDirector named " + aDirector.name + " is now stopped.");
            //player.GetComponent<PlayerMovement>().currentState = PlayerState.idle;
            FirstTime = false;
            //dialogBox.SetActive(false);

            // Player reached location, now show dialogue of boss
            StartCoroutine(showBossDialog());
        }
    }

    private IEnumerator showBossDialog()
    { 
        // Set for boss dialog
        for (int i = 0; i < bossDialogList.StringList.Count; i++)
        {
            dialogText.text = bossDialogList.StringList[i];
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        dialogBox.SetActive(false);

        // Done with boss dialog
        changeMusic(creditAudio);
        transitToBlackPanel.GetComponent<Animator>().SetBool("Credit", true);
        for (int i = 0; i < creditDialogList.StringList.Count; i++)
        {
            transitToBlackPanel.GetComponentInChildren<Text>().text = creditDialogList.StringList[i];
            yield return new WaitForSeconds(20f);
        }
    }


    /*private IEnumerator waitForKeyPress()
    {
        bool done = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            if (Input.GetButtonDown("Check"))
            {
                done = true; // breaks the loop
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }
    }*/
     
    void OnEnable()
    {
        timeline.stopped += OnPlayableDirectorStopped;
    }


    void OnDisable()
    {
        timeline.stopped -= OnPlayableDirectorStopped;
    }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         timeline.Stop();
    //     }
    // }

}
