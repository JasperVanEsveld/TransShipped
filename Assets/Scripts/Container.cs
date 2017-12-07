public class Container
{
    public Container(bool isFridge, int transTyep)
    {
        this.isFridge = isFridge;
        this.transTyep = transTyep;
    }

    private bool isFridge { get; set; }

    private int transTyep { get; set; }
}