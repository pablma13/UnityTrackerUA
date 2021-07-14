
public abstract class IPersistence
{
    public IPersistence(ISerializer ser) { serializer = ser; }

    ISerializer serializer;

    public abstract void Send(TrackerEvent te);

    //public abstract void Flush();
}
