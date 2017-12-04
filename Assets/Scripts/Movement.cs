public class Movement
{
    /// <summary>
    /// The container described in this movement
    /// </summary>
    private readonly MonoContainer _container;

    /// <summary>
    /// Starting point of this movement
    /// </summary>
    private readonly Area _originArea;

    /// <summary>
    /// Desitnation of this movement
    /// </summary>
    private readonly Area _targetArea;

    /// <summary>
    /// Constructor of a movement with 3 parameters
    /// </summary>
    /// <param name="container">The container described in this movement</param>
    /// <param name="originArea">Starting point of this movement</param>
    /// <param name="targetArea">Destination of this movement</param>
    public Movement(MonoContainer container, Area originArea, Area targetArea)
    {
        _container = container;
        _originArea = originArea;
        _targetArea = targetArea;
    }

    public MonoContainer Container
    {
        get { return _container; }
    }

    public Area OriginArea
    {
        get { return _originArea; }
    }

    public Area TargetArea
    {
        get { return _targetArea; }
    }
}