using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    public JSONManager jsonManager;
    //public Dropdown dropdown;
    //public Text TextBox;

    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        jsonManager.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JournalUpdate(string text)
    {
        jsonManager.data.Entry = text;
        Debug.Log("Journal Entry:" + text);
    }

    public void ClickSubmit(string text)
    {
        jsonManager.data.Date = DateTime.Now.ToString("dd/MM/yy");
        Debug.Log(jsonManager.data.Date + ": " + jsonManager.data.Entry);

        


        //jsonManager.journal.Journal.Add(new JournalData() { Date = jsonManager.data.Date, Entry = jsonManager.data.Entry });

        //Debug.Log("journal.Journal:" )

        jsonManager.Save();
        jsonManager.Load();
    }

    public void DisableMovement()
    {
        player.GetComponent<PlayerMovement>().currentState = PlayerState.interact;
    }

    public void EnableMovement()
    {
        player.GetComponent<PlayerMovement>().currentState = PlayerState.idle;
    }
}
