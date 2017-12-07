public class Movement
{
    /// <summary>
    /// The container described in this movement
    /// </summary>
    private readonly MonoContainer container;

    /// <summary>
    /// Starting point of this movement
    /// </summary>
    private readonly Area originArea;

    /// <summary>
    /// Desitnation of this movement
    /// </summary>
    private readonly Area targetArea;

    /// <summary>
    /// Constructor of a movement with 3 parameters
    /// </summary>
    /// <param name="container">The container described in this movement</param>
    /// <param name="originArea">Starting point of this movement</param>
    /// <param name="targetArea">Destination of this movement</param>
    public Movement(MonoContainer container, Area originArea, Area targetArea)
    {
        this.container = container;
        this.originArea = originArea;
        this.targetArea = targetArea;
    }

    public Area TargetArea
    {
        get { return targetArea; }
    }
}