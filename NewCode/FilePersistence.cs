
public class FilePersistence : IPersistence
{
    System.IO.StreamWriter _writer;
    //JSONSerializer serializer = new JSONSerializer();

    //llamar al del padre
    public FilePersistence(ISerializer ser)
    {
        IPersistence(ser);
    }

    //public override void Flush()
    //{
    //    throw new System.NotImplementedException();
    //}

    public override void Send(TrackerEvent te)
    {
        _writer = new System.IO.StreamWriter(Tracker.Instance.GetDataPath() + "/" + te.GetPath(), true);
        _writer.WriteLine(serializer.Serialize(te));
        _writer.Close();
    }
}
