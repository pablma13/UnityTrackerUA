using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region EVENT CLASSES ----------------------------------------------------------------------------------------

/// <summary>
/// Base class for all the tracker events. Contains shared attributes
/// and functionality
/// </summary>
class Event
{
    public float _timestamp;
    public int _playerID;

    protected string _path;

    public Event() { }

    public Event(int playerID) 
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


/// <summary>
/// Derived class that provides information about a killed enemy event
/// </summary>
class KillEvent : Event
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


/// <summary>
/// Derived class that provides information about any pickup that the player picks
/// </summary>
class PickupEvent : Event
{
    public PickupEvent(int playerID)
    {
        _path = "PickupEvent.json";
        Init(playerID);
    }
}


/// <summary>
/// Derived class that provides information about a dead player event
/// </summary>
class PlayerDiedEvent : Event
{
    public PlayerDiedEvent(int playerID)
    {
        _path = "PlayerDiedEvent.json";
        Init(playerID);
    }
}


/// <summary>
/// Derived class that provides information about the points earned by the player when the match is over
/// </summary>
class PointsEarnedEvent : Event
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

#endregion



#region TRACKER ----------------------------------------------------------------------------------------------

/// <summary>
/// Tracker class. Deals with the event creation and their parsing to json
/// </summary>
public static class Tracker
{
    #region ATTRIBUTES
    static private KillEvent _killEvent = null;
    static private PickupEvent _pickupEvent = null;
    static private PlayerDiedEvent _deadEvent = null;
    static private PointsEarnedEvent _pointsEvent = null;
    
    static private System.IO.StreamWriter _writer;
    #endregion

    #region METHODS
    /// <summary>
    /// Internal method used to parse and write event info into a .json file
    /// </summary>
    /// <param name="e"></param>
    private static void WriteToFile(Event e)
    {
        _writer = new System.IO.StreamWriter(Application.persistentDataPath + "/" + e.GetPath(), true);
        _writer.WriteLine(JsonUtility.ToJson(e));
        _writer.Close();
    }

   
    /// <summary>
    /// Tracks the "Enemy Killed" event and parses the info to json
    /// </summary>
    /// <param name="timestamp">Time in seconds from the beginning of the session</param>
    /// <param name="playerID">ID of the player that performed the kill</param>
    /// <param name="deadPlayerID">ID of the player that was killed</param>
    public static void EnemyKill(float timestamp, int playerID, int deadPlayerID) {
        // if the event is yet to initialize, we create a new one
        if (_killEvent == null)
            _killEvent = new KillEvent(playerID);

        // updates the internal data of the event
        _killEvent.UpdateInfo(timestamp, deadPlayerID);

        // parses the event to json and writes it in a .json file
        WriteToFile(_killEvent);
    }


    /// <summary>
    /// Tracks the "Pickup" event and parses the info to json
    /// </summary>
    /// <param name="timestamp">Time in seconds from the beginning of the session</param>
    /// <param name="playerID">ID of the player that performed the kill</param>
    public static void Pickup(float timestamp, int playerID)
    {
        // if the event is yet to initialize, we create a new one
        if (_pickupEvent == null)
            _pickupEvent = new PickupEvent(playerID);

        // updates the internal data of the event
        _pickupEvent.UpdateInfo(timestamp);

        // parses the event to json and writes it in a .json file
        WriteToFile(_pickupEvent);
    }


    /// <summary>
    /// Tracks the "Player Died" event and parses the event to json
    /// </summary>
    /// <param name="timestamp">Time in seconds from the beginning of the session</param>
    /// <param name="playerID">ID of the player that performed the kill</param>
    public static void PlayerDied(float timestamp, int playerID)
    {
        // if the event is yet to initialize, we create a new one
        if (_deadEvent == null)
            _deadEvent = new PlayerDiedEvent(playerID);

        // updates the internal data of the event
        _deadEvent.UpdateInfo(timestamp);

        // parses the event to json and writes it in a .json file
        WriteToFile(_deadEvent);
    }


    /// <summary>
    /// Tracks the "Points Earned" event and parses the event to json
    /// </summary>
    /// <param name="timestamp">Time in seconds from the beginning of the session</param>
    /// <param name="playerID">ID of the player that performed the kill</param>
    /// <param name="points">Points earned by the player at the end of the match</param>
    public static void PointsEarned(float timestamp, int playerID, int points)
    {
        // if the event is yet to initialize, we create a new one
        if (_pointsEvent == null)
            _pointsEvent = new PointsEarnedEvent(playerID);

        // updates the internal data of the event
        _pointsEvent.UpdateInfo(timestamp);

        // parses the event to json and writes it in a .json file
        WriteToFile(_pointsEvent);
    }
    #endregion
}

#endregion--------

