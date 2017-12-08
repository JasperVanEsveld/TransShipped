public class Container
{
    public Container(bool isFridge, int transTyep)
    {
        this.isFridge = isFridge;
        this.transTyep = transTyep;
    }

    public bool isFridge { get; set; }

    public int transTyep { get; set; }
}