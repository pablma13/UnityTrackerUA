using UnityEngine;

/// <summary>
/// Derived class that provides information about a killed enemy event
/// </summary>
class KillEvent : TrackerEvent
{
    public int _deadPlayerID;

    public KillEvent(int playerID, float timestamp, int deadPlayerID) : base(playerID, timestamp)
    {
        _eventType = TrackerEventType.KILL;
        _path = "KillerEvents.json";
        _deadPlayerID = deadPlayerID;
    }
}