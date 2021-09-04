using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DDLoad : MonoBehaviour
{
    public JSONManager jsonManager;
    public Text TextBox;

    // Start is called before the first frame update
    void Start()
    {
        //print("Loading DD");
        var dropdown = transform.GetComponent<Dropdown>();

        //List<string> entryDates = new List<string>();

        
        //dropdown.options.Clear();

        //foreach (JournalData data in jsonManager.journal.Journal)
        //{
        //    entryDates.Add(data.Date);
        //    dropdown.options.Add(new Dropdown.OptionData() { text = data.Date });
        //    //print(data.Date);
        //}
        //TextBox.text = jsonManager.journal.Journal[0].Entry;
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });

        
        //dropdown.GetComponent<Dropdown>();
        //dropdown.ClearOptions();
        //dropdown.AddOptions(entryDates);

    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        TextBox.text = jsonManager.journal.Journal[index].Entry;
    }
    private void Update()
    {
        
    }
}
