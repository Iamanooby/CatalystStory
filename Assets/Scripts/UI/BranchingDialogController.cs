using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;

public class BranchingDialogController : MonoBehaviour
{
    [SerializeField] private GameObject branchingCanvas;
    [SerializeField] private GameObject dialogPrefab;
    [SerializeField] private GameObject choicePrefab;
    [SerializeField] private TextAssetValue dialogValue;
    [SerializeField] private Story myStory;
    [SerializeField] private GameObject dialogHolder;
    [SerializeField] private GameObject choiceHolder;
    [SerializeField] private ScrollRect dialogScroll;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableCanvas()
    {
        branchingCanvas.SetActive(true);
        SetStory();
        RefreshView();
    }


    public void SetStory()
    {
        if (dialogValue.initialValue)
        {
            DeleteOldDialogs();
            myStory = new Story(dialogValue.initialValue.text);
        }
        else
        {
            Debug.Log("Story setup error");
        }
    }


    void DeleteOldDialogs()
    {
        for (int i = 0; i < dialogHolder.transform.childCount; i ++)
        {
            Destroy(dialogHolder.transform.GetChild(i).gameObject);
        }
    }


    public void RefreshView()
    {
        // To check if still got story to continue!!
        // Story continues!! :)
        while(myStory.canContinue)
        {
            MakeNewDialog(myStory.Continue());
        }
        if (myStory.currentChoices.Count > 0)
        {
            MakeNewChoices();
        }
        // When story ENDS!! :(
        else
        {
            player.GetComponent<PlayerMovement>().currentState = PlayerState.idle;
            branchingCanvas.SetActive(false);

        }
        StartCoroutine(ScrollCo());
    }


    IEnumerator ScrollCo()
    {
        yield return null;
        dialogScroll.verticalNormalizedPosition = 0f;
    }


    void MakeNewDialog(string newDialog)
    {
        DialogObject newDialogObject = Instantiate(dialogPrefab, dialogHolder.transform).GetComponent<DialogObject>();
        newDialogObject.setup(newDialog);
    }


    void MakeNewResponse(string newDialog, int choiceValue)
    {
        ResponseObject newResponseObject = Instantiate(choicePrefab, choiceHolder.transform).GetComponent<ResponseObject>();
        newResponseObject.setup(newDialog, choiceValue);
        Button responseButton = newResponseObject.gameObject.GetComponent<Button>();
        if (responseButton)
        {
            responseButton.onClick.AddListener(delegate { ChooseChoice(choiceValue); });
        }

    }


    void MakeNewChoices()
    {
        for (int i = 0; i < choiceHolder.transform.childCount; i++)
        {
            Destroy(choiceHolder.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < myStory.currentChoices.Count; i++)
        {
            MakeNewResponse(myStory.currentChoices[i].text, i);
        }
    }


    void ChooseChoice(int choice)
    {
        myStory.ChooseChoiceIndex(choice);
        RefreshView();
    }
}
