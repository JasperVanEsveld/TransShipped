public class AreaSelectState : TutorialState {
    public AreaSelectState(Tutorial tutorial) : base(tutorial) {
        text =
            "Now choose which berth you want the ship to go to.";
    }

    private void DeliveryAreaSelected() {
        tut.currentState = new StackSelectState(tut);
    }

    private void StageComplete(){
        tut.currentState = new TutorialFailed(tut);
    }

    public override void BindAll() {
        StageEndState.StageComplete += StageComplete;
        CommandPanel.DeliveryAreaSelected += DeliveryAreaSelected;
    }

    public override void UnBindAll() {
        StageEndState.StageComplete -= StageComplete;
        CommandPanel.DeliveryAreaSelected -= DeliveryAreaSelected;
    }
}