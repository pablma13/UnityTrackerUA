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
        //_timestamp = Tracker.Instance.knowTime();
        //_deadPlayerID = Tracker.Instance._enemyID;
        //Init(Tracker.Instance._playerID);
    }
    /*
    public void UpdateInfo(float timestamp, int deadPlayerID) 
    {
        _timestamp = timestamp;
        _deadPlayerID = deadPlayerID;
    }*/
}