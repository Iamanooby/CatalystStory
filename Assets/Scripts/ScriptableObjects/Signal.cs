using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Signal : ScriptableObject
{
    // Create a list of SignalListeners
    public List<SignalListener> listeners = new List<SignalListener>();

    public void Raise()
    {
        // Raise a method 
        // Count backwards to prevent error if signalListeners are destroyed
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener)
    {
        // Add SignalListener to list
        listeners.Add(listener);
    }

    public void DeRegisterListener(SignalListener listener)
    {
        // Remove SignalListener from list
        listeners.Remove(listener);
    }
}
