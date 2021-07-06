using UnityEngine;

static class Writer
{
    static private System.IO.StreamWriter _writer;

    /// <summary>
    /// Internal method used to parse and write event info into a .json file
    /// </summary>
    /// <param name="e">Event we want to parse and write to a file</param>
    public static void WriteToFile(TrackerEvent e)
    {
        _writer = new System.IO.StreamWriter(Tracker._dataPath + "/" + e.GetPath(), true);
        _writer.WriteLine(JsonUtility.ToJson(e));
        _writer.Close();
    }
}
