public class ShipSelectState : TutorialState {
    public ShipSelectState(Tutorial tutorial) : base(tutorial) {
        text =
            "There is a ship waiting let it in by clicking the button.";
    }

    private void StageComplete(){
        tut.currentState = new TutorialFailed(tut);
    }

    private void VehicleSelected() {
        tut.currentState = new AreaSelectState(tut);
    }

    public override void BindAll() {
        CommandPanel.VehicleSelected += VehicleSelected;
        StageEndState.StageComplete += StageComplete;
    }

    public override void UnBindAll() {
        CommandPanel.VehicleSelected -= VehicleSelected;
        StageEndState.StageComplete -= StageComplete;
    }
}