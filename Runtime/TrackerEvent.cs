using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all the tracker events. Contains shared attributes
/// and functionality
/// </summary>
public class TrackerEvent
{
    public enum TrackerEventType { EXIT, KILL, OPEN, PICKUP, DIED, POINTS};

    public TrackerEventType _eventType;
    public float _timestamp;
    public int _playerID;

    protected string _path;

    public TrackerEvent() { }

    public TrackerEvent(int playerID, float timestamp) //áñadir  timestamp y argumentos extra por cada tipo de evento
    {
        _playerID = playerID;
        _timestamp = timestamp;
    }

    //public virtual void Init(int playerID)
    //{
    //    _playerID = playerID;
    //}

    //// TODO: revisar luego todos los metoddos y quitar esto, meter en el constructor
    //public virtual void UpdateInfo(float timestamp)
    //{
    //    _timestamp = timestamp;
    //}

    public string GetPath()
    {
        return _path;
    }
}
