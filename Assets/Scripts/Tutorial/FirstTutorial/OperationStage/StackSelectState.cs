public class StackSelectState : TutorialState {
    public StackSelectState(Tutorial tutorial) : base(tutorial) {
        text =
            "Now choose which stack you want the containers to go to, for now you just have one.";
    }

    private void StackSelected() {
        tut.currentState = new ShipSuccessState(tut);
    }

    private void StageComplete(){
        tut.currentState = new TutorialFailed(tut);
    }

    public override void BindAll() {
        StageEndState.StageComplete += StageComplete;
        CommandPanel.StackSelected += StackSelected;
    }

    public override void UnBindAll() {
        StageEndState.StageComplete -= StageComplete;
        CommandPanel.StackSelected -= StackSelected;
    }
}