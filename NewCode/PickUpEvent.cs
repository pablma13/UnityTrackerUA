using UnityEngine;

/// <summary>
/// Derived class that provides information about any pickup that the player picks
/// </summary>
class PickupEvent : TrackerEvent
{
    public PickupEvent()
    {
        _path = "PickupEvent.json";
        _timestamp = Tracker.Instance.knowTime();
        Init(Tracker.Instance._playerID);
    }
}