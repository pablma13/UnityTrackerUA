using UnityEngine;

/// <summary>
/// Derived class that provides information about any pickup that the player picks
/// </summary>
class PickupEvent : TrackerEvent
{
    public PickupEvent(int playerID, float timestamp): base(playerID, timestamp)
    {
        _eventType = TrackerEventType.PICKUP;
        _path = "PickupEvent.json";
    }
}