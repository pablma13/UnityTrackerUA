
public abstract class IPersistence
{
    public IPersistence(ISerializer ser) 
    { 
        serializer = ser; 
    }

    protected ISerializer serializer;

    public abstract void Send(TrackerEvent te);
}
