using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
/*
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

#region GENERIC EVENTS ---------------------------------------------------------------------------------------

/// <summary>
/// Derived class that tracks exit events.
/// </summary>
class ExitEvent : Event
{
    public ExitEvent()
    {
        _path = "ExitEvent.json";
        Init(0);
    }

}

/// <summary>
/// Derived class that tracks open events.
/// </summary>
class OpenEvent : Event
{
    public string _startSceneName;
    public OpenEvent()
    {
        _path = "OpenEvent.json";
        Init(0);
    }
}

#endregion

#region AUTOMATIC EVENTS -------------------------------------------------------------------------------------
/// <summary>
/// Base class for all the tracker automatic events.
/// </summary>
class AutomaticEvent : Event
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

/// <summary>
/// Example of the work of an automatic event that tracks when the x position
/// of a tracked object goes outside a given range
/// </summary>
class AutomaticEventExample : AutomaticEvent
{
    private string _followedName;
    private int _maxPosX = 300;
    public Vector3 _pos;

    public AutomaticEventExample() { }

    public override void Update() 
    {
        _pos = GameObject.Find(_followedName).GetComponent<Transform>().position;
        _writePending = _pos.x > _maxPosX;
    }

    public AutomaticEventExample(int playerID)
    {
        _path = "AutomaticEvent.json";
        Init(playerID);
    }

    public void setFollowedObject(string followedName)
    {
        _followedName = followedName;
    }
}
#endregion

#region PROJECT-SPECIFIC EVENTS ------------------------------------------------------------------------------
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
#endregion
*/
#region TRACKER ----------------------------------------------------------------------------------------------

/// <summary>
/// Tracker class. Deals with the event creation, management and parsing to json
/// </summary>
public static class Tracker
{
    #region ATTRIBUTES ---------------------------------------------------------------------------------------

    // Automatic event management
    static List<TrackerEvent> _eventsToWrite = new List<TrackerEvent>();
    static List<OpenEvent> _openEvents = new List<OpenEvent>();
    static List<ExitEvent> _exitEvents = new List<ExitEvent>();
    static List<AutomaticEvent> _automaticEvents = new List<AutomaticEvent>();

    static private string _activeScene = null;
    static public string _dataPath = null;
    static private float _time;

    static private bool _exit = false;
    static private System.IO.StreamWriter _writer;
    #endregion



    #region METHODS ------------------------------------------------------------------------------------------
    
    

    #region THREAD MANAGEMENT --------------------------------------------------------------------------------
    /// </summary>
    /// Activate tracker's thread
    /// </summary>
    public static void Init()
    {
        _dataPath = Application.persistentDataPath;
        Thread t = new Thread(new ThreadStart(ThreadUpdate));
        t.Start();
    }


    /// </summary>
    /// Update the active tracker's thread
    /// </summary>
    private static void ThreadUpdate()
    {
        //Check scenes
        while (!_exit)
        {
            //Open events tracking
            foreach (OpenEvent e in _openEvents)
            {
                if (e._startSceneName == _activeScene)
                {
                    e._timestamp = _time;
                    _eventsToWrite.Add(e);
                }
            }
            foreach (TrackerEvent e in _eventsToWrite)
            {
                Writer.WriteToFile(e);

                if (e is OpenEvent)
                {
                    _openEvents.Remove((OpenEvent)e);
                }
            }
            _eventsToWrite.Clear();
            Thread.Sleep(10);
        }
        //Exit events tracking
    }


    /// <summary>
    /// Goes through the list of automatic events and, if they're pending write,
    /// adds them to the write list. This write list contains both automatic and 
    /// regular events.
    /// </summary>
    public static void Update()
    {
        _activeScene = SceneManager.GetActiveScene().name;
        _time = Time.time;
        foreach (AutomaticEvent e in _automaticEvents)
        {
            e.Update();

            if (e._writePending)
            {
                _eventsToWrite.Add(e);
                e._writePending = false;
            }
        }
    }


    /// </summary>
    /// Finish tracker's thread process and write exit events
    /// </summary>
    public static void Exit()
    {
        _exit = true;

        foreach (ExitEvent e in _exitEvents)
        {
            e._timestamp = _time;
            Writer.WriteToFile(e);
        }
    }
    #endregion


    #region GENERIC EVENTS -----------------------------------------------------------------------------------
    /// <summary>
    /// Example of an Exit Event
    /// </summary>
    /// <param name="timestamp">Time in seconds from the beginning of the session</param>
    /// <param name="playerID">ID of the player</param>   
    public static void ExitEventExample()
    {
        //Create a new event
        ExitEvent e = new ExitEvent();

        // updates the internal data of the event
        e.UpdateInfo(0);

        //Add the event to the list
        _exitEvents.Add(e);
    }


    /// <summary>
    /// Example of an OpenEvent
    /// </summary>
    /// <param name="timestamp">Time in seconds from the beginning of the session</param>
    /// <param name="playerID">ID of the player</param>
    /// <param name="sceneName">Name of the scene we are opening</param>
    public static void OpenEventExample(string sceneName)
    {
        //Create a new event
        OpenEvent e = new OpenEvent();

        // updates the internal data of the event
        e.UpdateInfo(0);
        e._startSceneName = sceneName;

        //Add the event to the list
        _openEvents.Add(e);
    }
    #endregion


    #region PROJECT-SPECIFIC EVENTS --------------------------------------------------------------------------
    /// <summary>
    /// Tracks the "Enemy Killed" event and parses the info to json
    /// </summary>
    /// <param name="timestamp">Time in seconds from the beginning of the session</param>
    /// <param name="playerID">ID of the player that performed the kill</param>
    /// <param name="deadPlayerID">ID of the player that was killed</param>
    public static void EnemyKill(float timestamp, int playerID, int deadPlayerID) {
        // if the event is yet to initialize, we create a new one
        KillEvent _killEvent = new KillEvent(playerID);

        // updates the internal data of the event
        _killEvent.UpdateInfo(timestamp, deadPlayerID);

        // adds the event to the write list so the thread update writes them to a .json file
        _eventsToWrite.Add(_killEvent);
    }


    /// <summary>
    /// Tracks the "Pickup" event and parses the info to json
    /// </summary>
    /// <param name="timestamp">Time in seconds from the beginning of the session</param>
    /// <param name="playerID">ID of the player that performed the kill</param>
    public static void Pickup(float timestamp, int playerID)
    {
        // if the event is yet to initialize, we create a new one
        PickupEvent _pickupEvent = new PickupEvent(playerID);

        // updates the internal data of the event
        _pickupEvent.UpdateInfo(timestamp);

        // adds the event to the write list so the thread update writes them to a .json file
        _eventsToWrite.Add(_pickupEvent);
    }


    /// <summary>
    /// Tracks the "Player Died" event and parses the event to json
    /// </summary>
    /// <param name="timestamp">Time in seconds from the beginning of the session</param>
    /// <param name="playerID">ID of the player that performed the kill</param>
    public static void PlayerDied(float timestamp, int playerID)
    {
        // if the event is yet to initialize, we create a new one
        PlayerDiedEvent _deadEvent = new PlayerDiedEvent(playerID);

        // updates the internal data of the event
        _deadEvent.UpdateInfo(timestamp);

        // adds the event to the write list so the thread update writes them to a .json file
        _eventsToWrite.Add(_deadEvent);
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
        PointsEarnedEvent _pointsEvent = new PointsEarnedEvent(playerID);

        // updates the internal data of the event
        _pointsEvent.UpdateInfo(timestamp, points);

        // adds the event to the write list so the thread update writes them to a .json file
        _eventsToWrite.Add(_pointsEvent);
    }
    #endregion


    /// <summary>
    /// Internal method used to parse and write event info into a .json file
    /// </summary>
    /// <param name="e">Event we want to parse and write to a file</param>
    /*private static void WriteToFile(Event e)
    {
        _writer = new System.IO.StreamWriter(_dataPath + "/" + e.GetPath(), true);
        _writer.WriteLine(JsonUtility.ToJson(e));
        _writer.Close();
    }*/
    #endregion
}

#endregion
