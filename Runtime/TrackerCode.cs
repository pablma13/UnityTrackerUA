using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Event
{
    public int
        timestamp_,
        playerID_;
    public Event() { }
    public Event(int playerID) 
    {
        init (playerID);
    }
    public virtual void init(int playerID)
    {
        playerID_ = playerID;
    }
    public virtual void check() { }
}

class killEvent : Event
{
    public int
        deadPlayerID_;
    public killEvent(int playerID)
    {
        init(playerID);
    }
    public void check(int timestamp, int deadPlayerID) 
    {
        timestamp_ = timestamp;
        deadPlayerID_ = deadPlayerID;
    }
}

public static class Tracker
{
    static private killEvent kEvent_ = null;
    
    static private System.IO.StreamWriter writer_;
    public static void enemyKill(int timestamp, int playerID, int deadPlayerID) {
        if (kEvent_ == null)
            kEvent_ = new killEvent(playerID);
        kEvent_.check(timestamp, deadPlayerID);
        writer_ = new System.IO.StreamWriter("KillerEvents.json", true);
        writer_.WriteLine(JsonUtility.ToJson(kEvent_));
        writer_.Close();
    }
}


