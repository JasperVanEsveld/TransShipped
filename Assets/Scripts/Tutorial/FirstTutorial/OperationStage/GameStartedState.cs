public class GameStartedState : TutorialState {
    public GameStartedState(Tutorial tutorial) : base(tutorial) {
        text = "Exciting times! Your first operational container terminal!\n"
             + "Vehicles will be requesting access to your terminal and you can choose which ones you want to let in.";
    }

    private void OnVehcicleGen() {
        tut.currentState = new ShipSelectState(tut);
    }

    public override void BindAll() {
        OperationState.vehicleGeneratedEvent += OnVehcicleGen;
    }

    public override void UnBindAll() {
        OperationState.vehicleGeneratedEvent -= OnVehcicleGen;
    }
}