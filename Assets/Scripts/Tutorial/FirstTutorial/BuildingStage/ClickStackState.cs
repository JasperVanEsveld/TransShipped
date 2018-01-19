public class ClickStackState : TutorialState {
    public ClickStackState(Tutorial tutorial) : base(tutorial) {
        text = "The main purpose of the container terminal is to handle containers"
             + " so the first thing you will need is a stacking area where containers can be stored.\n"
             + "You can buy a new stacking area by selecting one of the green areas.";
    }

    private void GameStarted(){
        tut.currentState = new NoStackWarning(tut);
    }

    private void OptionalAreaClicked(OptionalArea area) {
        tut.currentState = new BuyStackState(tut);
    }

    public override void BindAll() {
        OptionalArea.MouseDownEvent += OptionalAreaClicked;
        BuildingPanel.GameStarted += GameStarted;
    }

    public override void UnBindAll() {
        OptionalArea.MouseDownEvent -= OptionalAreaClicked;
        BuildingPanel.GameStarted -= GameStarted;
    }
}