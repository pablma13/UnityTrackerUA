using UnityEngine;

/// <summary>
/// Derived class that provides information about a dead player event
/// </summary>
class PlayerDiedEvent : TrackerEvent
{
    public PlayerDiedEvent(int playerID)
    {
        _path = "PlayerDiedEvent.json";
        Init(playerID);
    }
}