using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class player_AI_OpenCutScence : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public PlayableDirector timeline; 
    public GameObject player;
    public StringListValue introDialog;
    public GameObject HUD;
    //public BoolValue gameStarting;
    public static bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    { 
        if(/*!gameStarting.initialValue &&*/ !gameStarted)
        {
            dialogText.text = "Good Morning, Welcome to the Catalyst";
            player.GetComponent<PlayerMovement>().currentState = PlayerState.interact;
            //gameStarting.initialValue = true;
            gameStarted = true;
            timeline.Play(); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (timeline == aDirector)
        {
            dialogBox.SetActive(true);
            player.GetComponent<PlayerMovement>().currentState = PlayerState.idle;
            StartCoroutine(showIntroDialog());            
        }
    }

    private IEnumerator showIntroDialog()
    {
        for (int i = 0; i < introDialog.StringList.Count; i++)
        {
            dialogText.text = introDialog.StringList[i];
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        dialogBox.SetActive(false);
    }


    void OnEnable()
    {
        timeline.stopped += OnPlayableDirectorStopped;
    }


    void OnDisable()
    {
        timeline.stopped -= OnPlayableDirectorStopped;
    }
}
