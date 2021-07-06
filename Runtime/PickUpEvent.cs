using UnityEngine;

/// <summary>
/// Derived class that provides information about any pickup that the player picks
/// </summary>
class PickupEvent : TrackerEvent
{
    public PickupEvent(int playerID)
    {
        _path = "PickupEvent.json";
        Init(playerID);
    }
}