public class ShipSelectState : TutorialState {
    public ShipSelectState(Tutorial tutorial) : base(tutorial) {
        text =
            "There is a ship waiting let it in by first clicking the button then the prefered berth.\n"
          + "After that press the stack to store the containers in that area. ";
    }

    private void VehicleSelected() {
        tut.currentState = new AreaSelectState(tut);
    }

    public override void BindAll() {
        CommandPanel.VehicleSelected += VehicleSelected;
    }

    public override void UnBindAll() {
        CommandPanel.VehicleSelected -= VehicleSelected;
    }
}