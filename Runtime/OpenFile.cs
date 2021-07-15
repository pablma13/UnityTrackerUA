using UnityEngine;

/// <summary>
/// Derived class that tracks open events.
/// </summary>
class OpenEvent : TrackerEvent
{
    public string _startSceneName;
    public OpenEvent(int playerID, float timestamp, string startSceneName) : base(playerID, timestamp)
    {
        _eventType = TrackerEventType.OPEN;
        _path = "OpenEvent.json";
        _startSceneName = startSceneName;
    }
}