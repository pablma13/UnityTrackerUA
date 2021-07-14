using System.Collections;
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
    // todos a la misma
    List<OpenEvent> _openEvents = new List<OpenEvent>();
    List<ExitEvent> _exitEvents = new List<ExitEvent>();
    List<AutomaticEvent> _automaticEvents = new List<AutomaticEvent>();
    
    IPersistence persistenceObj;
    List<TrackerEvent> activeEvents;

    string _activeScene = null;
    string _dataPath = null;
    float _time;
    bool _exit = false;

    // quiza pasar por aquí algo para el tiempo:
    //      o el timestamp de unity
    //      o cero y contar desde dentro del proyecto
    //          usar epoch (usar el system time)
    public void Init(/*aqui si tuvieramos otros tipos de persistencia, datos sobre cual usar*/)
    {
        persistenceObj = new FilePersistence(new JSONSerializer());
        _dataPath = Application.persistentDataPath;
        // semaforo para la cola para thread safety
        // aunque hebra opcional
        // cambiar esto a solo mandar temporizado o final del juego
        Thread t = new Thread(new ThreadStart(ThreadUpdate)); 
        t.Start();
    }

    /// </summary>
    /// Update the active tracker's thread
    /// </summary>
    private void ThreadUpdate()
    {
        //Check scenes
        while (!_exit)
        {
            //Open events tracking
            foreach (OpenEvent e in _openEvents) // este no pinta aquí
            {
                if (e._startSceneName == _activeScene)
                {
                    e._timestamp = _time;
                    _eventsToWrite.Add(e);
                }
            }
            foreach (TrackerEvent e in _eventsToWrite) // este sí
            {
                persistenceObj.Send(e);
                if (e is OpenEvent) // no tratar de especial
                {
                    _openEvents.Remove((OpenEvent)e);
                }
            }
            _eventsToWrite.Clear();
            Thread.Sleep(10);
        }
        //Exit events tracking
    }

    public void End()
    {
        _exit = true;

        // llamar al flush para limpiar eventos que queden

        foreach (ExitEvent e in _exitEvents)
        {
            e._timestamp = _time;
            persistenceObj.Send(e);
        }
    }

    // TODO: usar llamadas de antes pero quitar el timestamp y el id

    // añadir el timestamp y el id aqui dentro, no meterlo como argumentos en la creacion de eventos
    public void TrackEvent(TrackerEvent.TrackerEventType eventType)
    {
        switch (eventType)
        {
            case TrackerEvent.TrackerEventType.KILL:

                break;
            case TrackerEvent.TrackerEventType.OPEN:
                break;
            case TrackerEvent.TrackerEventType.PICKUP:
                break;
            case TrackerEvent.TrackerEventType.DIED:
                break;
            case TrackerEvent.TrackerEventType.POINTS:
                break;
            default:
                break;
        }
    }

    public string GetDataPath()
    {
        return _dataPath;
    }
}
