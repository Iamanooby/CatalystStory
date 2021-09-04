using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JournalData
{
    public string Date;

    public string Entry;

}
[System.Serializable] 
public class Root
{
    public List<JournalData> Journal;
}