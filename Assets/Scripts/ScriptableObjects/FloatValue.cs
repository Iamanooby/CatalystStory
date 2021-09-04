using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Values can be read through multiple scenes
// (Doesnt get reloaded when scene has been stopped)

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;
    public float defaultValue;
    // -1 to disable defaultValue

    [HideInInspector]
    public float RuntimeValue; 
    // Runtime Value: When game is run the value would be serialised so that we can deduct from player's current health


    public void OnAfterDeserialize()
    {
        // Game End
        if (defaultValue != -1)
        {
            initialValue = defaultValue;
        }
        RuntimeValue = initialValue;

    }

    public void OnBeforeSerialize()
    {
        // Game Start
    }
}
