using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceGenerationEventSO : MonoBehaviour
{
    private List<AdvanceGenerationEventListener> listeners =
       new List<AdvanceGenerationEventListener>();

    public void Raise(OwnerSO owner)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(owner);
    }

    public void RegisterListener(AdvanceGenerationEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(AdvanceGenerationEventListener listener)
    { listeners.Remove(listener); }
}
