using UnityEngine;

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