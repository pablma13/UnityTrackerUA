using UnityEngine;

/// <summary>
/// Base class for all the tracker automatic events.
/// </summary>
class AutomaticEvent : TrackerEvent
{
    public bool _writePending = false;

    public AutomaticEvent() { }

    public virtual void Update() { }

    public AutomaticEvent(int playerID)
    {
        _path = "AutomaticEvent.json";
        Init(playerID);
    }
}