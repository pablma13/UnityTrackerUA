
public class FilePersistence : IPersistence
{
    System.IO.StreamWriter _writer;
    public FilePersistence(ISerializer ser) :base(ser) { }


    public override void Send(TrackerEvent te)
    {
        _writer = new System.IO.StreamWriter(Tracker.Instance.GetDataPath() + "/" + te.GetPath(), true);
        _writer.WriteLine(serializer.Serialize(te));
        _writer.Close();
    }
}
