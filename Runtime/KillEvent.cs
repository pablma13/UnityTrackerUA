using UnityEngine;

/// <summary>
/// Derived class that provides information about a killed enemy event
/// </summary>
class KillEvent : TrackerEvent
{
    public int _deadPlayerID;

    public KillEvent(int playerID)
    {
        _path = "KillerEvents.json";
        Init(playerID);
    }

    public void UpdateInfo(float timestamp, int deadPlayerID) 
    {
        _timestamp = timestamp;
        _deadPlayerID = deadPlayerID;
    }
}