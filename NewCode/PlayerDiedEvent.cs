using UnityEngine;

/// <summary>
/// Derived class that provides information about a dead player event
/// </summary>
class PlayerDiedEvent : TrackerEvent
{
    public PlayerDiedEvent()
    {
        _path = "PlayerDiedEvent.json";
        Init(Tracker.Instance._playerID);
    }
}