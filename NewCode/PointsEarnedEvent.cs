using UnityEngine;

/// <summary>
/// Derived class that provides information about the points earned by the player when the match is over
/// </summary>
class PointsEarnedEvent : TrackerEvent 
{
    public int _points;

    public PointsEarnedEvent()
    {
        _path = "PointsEarnedEvent.json";
        _timestamp = Tracker.Instance.knowTime();
        Init(Tracker.Instance._playerID);
    }
    /*
    public void UpdateInfo(float timestamp, int points)
    {
        _timestamp = timestamp;
        _points = points;
    }*/
}