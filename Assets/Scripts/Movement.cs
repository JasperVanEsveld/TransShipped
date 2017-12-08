public class Movement
{
    /// <summary>
    /// Desitnation of this movement
    /// </summary>
    private readonly Area targetArea;

    public Area originArea { get; set; }
    /// <summary>
    /// Constructor of a movement with 3 parameters
    /// </summary>
    /// <param name="targetArea">Destination of this movement</param>
    public Movement(Area targetArea)
    {
        this.targetArea = targetArea;
    }

    public Area TargetArea
    {
        get { return targetArea; }
    }
}