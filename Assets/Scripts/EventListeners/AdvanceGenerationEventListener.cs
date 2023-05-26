using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceGenerationEventListener : MonoBehaviour
{
    public AdvanceGenerationEventSO Event;
    public AdvanceGenerationEvent Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(OwnerSO owner)
    { Response.Invoke(owner); }
}