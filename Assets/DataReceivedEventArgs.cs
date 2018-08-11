using System.Net.Sockets;

public class DataReceivedEventArgs
{
    private object data;

    public DataReceivedEventArgs(object data)
    {
        this.data = data;
    }

    public object GetData()
    {
        return data;
    }
}