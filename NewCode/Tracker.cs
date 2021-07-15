using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Tracker 
{
    #region SINGLETON
    private static readonly Tracker instance = new Tracker();
    static Tracker()
    {
    }
    private Tracker()
    {
    }
    public static Tracker Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion
    
    List<TrackerEvent> _eventsToWrite = new List<TrackerEvent>();
    List<AutomaticEvent> _automaticEvents = new List<AutomaticEvent>();
    
    IPersistence persistenceObj;
    List<TrackerEvent> activeEvents;

    string _activeScene = null;
    string _dataPath = null;
    TimeSpan _ts;
    public int _playerID, _enemyID, _points;
    bool _exit = false;

    // quiza pasar por aquí algo para el tiempo:
    //      o el timestamp de unity
    //      o cero y contar desde dentro del proyecto
    //          usar epoch (usar el system time)
    public void Init(/*aqui si tuvieramos otros tipos de persistencia, datos sobre cual usar*/int playerID)
    {
        persistenceObj = new FilePersistence(new JSONSerializer());
        _dataPath = Application.persistentDataPath;
        // semaforo para la cola para thread safety
        // aunque hebra opcional
        // cambiar esto a solo mandar temporizado o final del juego
        Thread t = new Thread(new ThreadStart(Flush)); 
        t.Start();
    }
    public void updateEnemyID(int enemyID)
    {
        _enemyID = enemyID;
    }
    public void updatePoints(int points)
    {
        _points = points;
    }
    public int GetTime()
    {
        _ts = DateTime.Now - new DateTime(1970, 1, 1);
        return (int)_ts.TotalSeconds;
    }

    public string GetDataPath()
    {
        return _dataPath;
    }

    /// </summary>
    /// Update the active tracker's thread
    /// </summary>
    private void Flush()
    {
        //Check scenes
        while (!_exit)
        {
            /*
            //Open events tracking
            foreach (OpenEvent e in _openEvents) // este no pinta aquí
            {
                if (e._startSceneName == _activeScene)
                {
                    e._timestamp = _time;
                    _eventsToWrite.Add(e);
                }
            }*/
            foreach (TrackerEvent e in _eventsToWrite) // este sí
            {
                persistenceObj.Send(e);
                //if (e is OpenEvent) // no tratar de especial
                //{
                //    _openEvents.Remove((OpenEvent)e);
                //}
            }
            _eventsToWrite.Clear();
            Thread.Sleep(10);
        }
        //Exit events tracking
    }

    public void End()
    {
        Flush();
        _exit = true;
    }

    #region GENERIC EVENTS -----------------------------------------------------------------------------------
    /// <summary>
    /// Example of an Exit Event
    /// </summary>  
    public void ExitEventExample()
    {
        //Create a new event
        ExitEvent e = new ExitEvent(_playerID, GetTime());

        //Add the event to the list
        _eventsToWrite.Add(e);
        
    }


    /// <summary>
    /// Example of an OpenEvent
    /// </summary>
    /// <param name="sceneName">Name of the scene we are opening</param>
    public void OpenEventExample(string sceneName)
    {
        //Create a new event
        OpenEvent e = new OpenEvent(_playerID, GetTime(), sceneName);

        //Add the event to the list
        _eventsToWrite.Add(e);
    }
    #endregion


    #region PROJECT-SPECIFIC EVENTS --------------------------------------------------------------------------
    /// <summary>
    /// Tracks the "Enemy Killed" event and parses the info
    /// </summary>
    /// <param name="deadPlayerID">ID of the player that was killed</param>
    public void EnemyKill(int deadPlayerID)
    {
        // if the event is yet to initialize, we create a new one
        KillEvent _killEvent = new KillEvent(_playerID, GetTime(), deadPlayerID);

        // adds the event to the write list so the thread update writes them to a .json file
        _eventsToWrite.Add(_killEvent);
    }


    /// <summary>
    /// Tracks the "Pickup" event and parses the info 
    /// </summary>
    public void Pickup()
    {
        // if the event is yet to initialize, we create a new one
        PickupEvent _pickupEvent = new PickupEvent(_playerID, GetTime());

        // adds the event to the write list so the thread update writes them to a .json file
        _eventsToWrite.Add(_pickupEvent);
    }


    /// <summary>
    /// Tracks the "Player Died" event and parses the event 
    /// </summary>
    public void PlayerDied()
    {
        // if the event is yet to initialize, we create a new one
        PlayerDiedEvent _deadEvent = new PlayerDiedEvent(_playerID, GetTime());

        // adds the event to the write list so the thread update writes them to a .json file
        _eventsToWrite.Add(_deadEvent);
    }


    /// <summary>
    /// Tracks the "Points Earned" event and parses the event 
    /// </summary>
    /// <param name="points">Points earned by the player at the end of the match</param>
    public void PointsEarned(int points)
    {
        // if the event is yet to initialize, we create a new one
        PointsEarnedEvent _pointsEvent = new PointsEarnedEvent(_playerID, GetTime(), points);

        // adds the event to the write list so the thread update writes them to a .json file
        _eventsToWrite.Add(_pointsEvent);
    }
    #endregion

}
