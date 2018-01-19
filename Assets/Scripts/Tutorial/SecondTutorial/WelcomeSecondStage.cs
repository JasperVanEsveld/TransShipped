public class WelcomeSecondState : TutorialState {
    public WelcomeSecondState(Tutorial tutorial) : base(tutorial) {
        text =
            "Ah there is a new delivery vehicle you can use! \n"
            + "Trucks only take red containers just like ships only take blue containers. AGVs are used to move containers between these two areas.\n"
            + "Good luck!";
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