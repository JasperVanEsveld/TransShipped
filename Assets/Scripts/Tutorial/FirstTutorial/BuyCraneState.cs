public class BuyCraneState : TutorialState {
    public BuyCraneState(Tutorial tutorial) : base(tutorial) {
        text = "To now buy a crane you click the Buy button";
    }

    private void CraneBought(CraneArea area) {
        tut.currentState = new ReadyState(tut);
    }

    public override void BindAll() {
        CraneArea.CraneBought += CraneBought;
    }

    public override void UnBindAll() {
        CraneArea.CraneBought -= CraneBought;
    }
}