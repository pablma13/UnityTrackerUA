using UnityEngine;

/// <summary>
/// Derived class that tracks exit events.
/// </summary>
class ExitEvent : TrackerEvent
{
    public ExitEvent(int playerID, float timestamp): base(playerID, timestamp)
    {
        _eventType = TrackerEventType.EXIT;
        _path = "ExitEvent.json";
        //Init(0);
    }

}