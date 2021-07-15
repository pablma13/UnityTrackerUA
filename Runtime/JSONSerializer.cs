
using UnityEngine;

public class JSONSerializer : ISerializer
{
    public override string Serialize(TrackerEvent te)
    {
        return JsonUtility.ToJson(te);
    }
}
