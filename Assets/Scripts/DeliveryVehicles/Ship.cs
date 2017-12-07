public class Ship : DeliveryVehicle
{
    public ShipArea area;
    private int i;

    private void Start()
    {
        i = 0;
    }

    public void Update()
    {
        if (i != 0 || !(area.game.currentState is OperationState)) return;
        EnterTerminal();
        i++;
    }

    private void EnterTerminal()
    {
        area.OnVehicleEnter(this);
    }
}