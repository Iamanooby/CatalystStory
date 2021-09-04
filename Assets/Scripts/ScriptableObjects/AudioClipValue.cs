using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioClipValue : ScriptableObject,ISerializationCallbackReceiver
{
    public AudioClip initialValue;
    public AudioClip defaultValue;

    public void OnAfterDeserialize()
    {
        initialValue = defaultValue;
    }

    public void OnBeforeSerialize()
    {

    }


}
