using UnityEngine;

/// <summary>
/// Derived class that provides information about a dead player event
/// </summary>
class PlayerDiedEvent : TrackerEvent
{
    public PlayerDiedEvent(int playerID, float timestamp) : base(playerID, timestamp)
    {
        _eventType = TrackerEventType.DIED;
        _path = "PlayerDiedEvent.json";
    }
}