public class NoPathState : TutorialState {
    public NoPathState(Tutorial tutorial) : base(tutorial) {
        text =
            "Ah oh, looks like you have lead the ship to the wrong berth.\n"
            + "Watch out because ships that have to wait for too long to be handled will cost you money!";
    }

    private void StageComplete(){
        tut.currentState = new TutorialFailed(tut);
    }

    public override void BindAll() {
        StageEndState.StageComplete += StageComplete;
    }

    public override void UnBindAll() {
        StageEndState.StageComplete -= StageComplete;
    }
}