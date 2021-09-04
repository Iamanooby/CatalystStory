using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{
    public int initialValue;
    public int defaultValue;

    public void OnAfterDeserialize()
    {
        if (defaultValue != -1)
        {
            initialValue = defaultValue;
        }
    }
    public void OnBeforeSerialize()
    {

    }
}
