public class TutorialCompleteState : TutorialState {
    public TutorialCompleteState(Tutorial tutorial) : base(tutorial) {
        text =
            "You now know the basics try to pass the next stage by upgrading and/or buying cranes";
    }

    public void TutorialEnd(){
        tut.currentState = new TutorialEnd(tut);
    }

    public override void BindAll() {
        BuildingPanel.GameStarted += TutorialEnd;
    }

    public override void UnBindAll() {
        BuildingPanel.GameStarted -= TutorialEnd;
    }
}