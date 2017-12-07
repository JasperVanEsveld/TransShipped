public class Ship : DeliveryVehicle
{
    public ShipArea area;
    int i = 0;

    public void Update(){
        if(i ==0){
            EnterTerminal();
            i++;
        }
    }
    protected override void EnterTerminal()
    {
        area.OnVehicleEnter(this);
    }

    protected override void LeaveTerminal()
    {
    }
}
