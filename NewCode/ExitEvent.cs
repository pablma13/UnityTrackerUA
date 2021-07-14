using UnityEngine;

/// <summary>
/// Derived class that tracks exit events.
/// </summary>
class ExitEvent : TrackerEvent
{
    public ExitEvent()
    {
        _path = "ExitEvent.json";
        Init(0);
    }

}