using UnityEngine;

/// <summary>
/// Base class for all the tracker events. Contains shared attributes
/// and functionality
/// </summary>
class TrackerEvent
{
    public float _timestamp;
    public int _playerID;

    protected string _path;

    public TrackerEvent() { }

    public TrackerEvent(int playerID) 
    {
        Init (playerID);
    }

    public virtual void Init(int playerID)
    {
        _playerID = playerID;
    }

    public virtual void UpdateInfo(float timestamp) 
    {
        _timestamp = timestamp;
    }

    public string GetPath()
    {
        return _path;
    }
}
