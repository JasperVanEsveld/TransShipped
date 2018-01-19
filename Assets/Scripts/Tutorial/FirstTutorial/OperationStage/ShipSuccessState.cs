public class ShipSuccessState : TutorialState {
    public ShipSuccessState(Tutorial tutorial) : base(tutorial) {
        text =
            "Congratulations on your first ship! If you keep going you might be able to handle even bigger things!";
    }

    private void NoCrane(CraneArea area) {
        tut.currentState = new NoPathState(tut);
    }

    private void StageComplete() {
        tut.currentState = new TutorialCompleteState(tut);
    }

    public override void BindAll() {
        CraneArea.NoCraneWarning += NoCrane;
        StageEndState.StageComplete += StageComplete;
    }

    public override void UnBindAll() {
        CraneArea.NoCraneWarning -= NoCrane;
        StageEndState.StageComplete -= StageComplete;
    }
}