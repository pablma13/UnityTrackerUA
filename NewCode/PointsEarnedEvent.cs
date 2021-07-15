using UnityEngine;

/// <summary>
/// Derived class that provides information about the points earned by the player when the match is over
/// </summary>
class PointsEarnedEvent : TrackerEvent 
{
    public int _points;

    public PointsEarnedEvent(int playerID, float timestamp, int points) : base(playerID, timestamp)
    {
        _eventType = TrackerEventType.POINTS;
        _path = "PointsEarnedEvent.json";
        //_timestamp = Tracker.Instance.knowTime();
        //Init(Tracker.Instance._playerID);
        _points = points;
    }
    /*
    public void UpdateInfo(float timestamp, int points)
    {
        _timestamp = timestamp;
        _points = points;
    }*/
}