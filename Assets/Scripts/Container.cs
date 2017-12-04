public class Container
{
    /// <summary>
    /// This boolean value indicates whether a container needs to be plugged in
    /// </summary>
    private readonly bool _isFridge;

    /// <summary>
    /// This int value indicates the transportation type of the container:<br/>
    /// 1. one needs to be dispatched by a ship<br/>
    /// 2. one needs to be dispatched by a train<br/>
    /// 3. one needs to be dispatched by a truck<br/>
    /// Different types of containers will be colored differently later<br/>
    /// </summary>
    private readonly int _transTyep;

    public Container(bool isFridge, int transTyep)
    {
        _isFridge = isFridge;
        _transTyep = transTyep;
    }

    public bool IsFridge
    {
        get { return _isFridge; }
    }

    public int TransTyep
    {
        get { return _transTyep; }
    }
}