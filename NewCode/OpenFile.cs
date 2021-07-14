using UnityEngine;

/// <summary>
/// Derived class that tracks open events.
/// </summary>
class OpenEvent : TrackerEvent
{
    public string _startSceneName;
    public OpenEvent()
    {
        _path = "OpenEvent.json";
        Init(0);
    }
}