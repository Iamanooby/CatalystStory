using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class JSONManager : MonoBehaviour
{
    //public JournalEntries journalEntries = new JournalEntries();
    public Root journal;

    public JournalData data;

    public Text TextBox;
    public Dropdown dropdown;

    private string file = "journal.json";

    public void Save()
    {
        string JsontoString;
        //print(data.Entry + ":" + data.Date);

        if (TextBox.text == "No entries found")
        {
            Debug.Log("Journal is null");
            journal = new Root();
            journal.Journal = new List<JournalData>();
            journal.Journal.Add(data);

            JsontoString = JsonUtility.ToJson(journal);
            print(JsontoString);
            WriteToFile(file, JsontoString);
        }
        else
        {
            journal.Journal.Add(data);
            JsontoString = JsonUtility.ToJson(journal);
            print(JsontoString);
            WriteToFile(file, JsontoString);
        }
        
    }


    public void Load()
    {
        data = new JournalData();


        string json = ReadFromFile(file);
        //Debug.Log(json);

        dropdown.options.Clear();

        if (json != null)
        {
            journal = JsonUtility.FromJson<Root>(json);
            foreach (JournalData data in journal?.Journal)
            {
                dropdown.options.Add(new Dropdown.OptionData() { text = data.Date });
                //Debug.Log(data.Date + ": " + data.Entry);
            }
            TextBox.text = journal.Journal[0].Entry;
        }
        Debug.Log(journal.Journal.Count() + " Journal Entried Found");


    }

    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);
        print("Writing Journal.json");
        using (StreamWriter write = new StreamWriter(fileStream))
        {
            write.Write(json);
        }
    }

    private string ReadFromFile(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.LogWarning("JSON file not found");
            TextBox.text = "No entries found";
        }
            
        return "";
    }

    private string GetFilePath(string fileName)
    {
        Debug.Log(Application.persistentDataPath + "/" + fileName);
        return Application.persistentDataPath + "/" + fileName;
    }
}


