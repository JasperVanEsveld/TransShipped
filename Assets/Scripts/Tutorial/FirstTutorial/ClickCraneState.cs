public class ClickCraneState : TutorialState {
    public ClickCraneState(Tutorial tutorial) : base(tutorial) {
        text = "The stacking area is now operational.\n"
             + "Now to connect this area to one of the berths next to it we will need a crane.\n"
             + "Click on the strip between the berth and the stack";
    }

    private void CraneAreaClicked(CraneArea area) {
        tut.currentState = new BuyCraneState(tut);
    }

    public override void BindAll() {
        CraneArea.MouseDownEvent += CraneAreaClicked;
    }

    public override void UnBindAll() {
        CraneArea.MouseDownEvent -= CraneAreaClicked;
    }
}