public class ClickStackState : TutorialState {
    public ClickStackState(Tutorial tutorial) : base(tutorial) {
        text = "The main purpose of the container terminal is to handle containers\n"
             + "so the first thing the user needs is a stacking area where containers can be stored.\n"
             + "You can buy a new stacking area by selecting one of the green areas.";
    }

    private void OptionalAreaClicked(OptionalArea area) {
        tut.currentState = new BuyStackState(tut);
    }

    public override void BindAll() {
        OptionalArea.MouseDownEvent += OptionalAreaClicked;
    }

    public override void UnBindAll() {
        OptionalArea.MouseDownEvent -= OptionalAreaClicked;
    }
}