using UnityEngine;

/// <summary>
/// Derived class that provides information about the points earned by the player when the match is over
/// </summary>
class PointsEarnedEvent : TrackerEvent 
{
    public int _points;

    public PointsEarnedEvent(int playerID)
    {
        _path = "PointsEarnedEvent.json";
        Init(playerID);
    }

    public void UpdateInfo(float timestamp, int points)
    {
        _timestamp = timestamp;
        _points = points;
    }
}