public class NoStackWarning : TutorialState {
    public NoStackWarning(Tutorial tutorial) : base(tutorial) {
        text = "Hm without a stack area to store the containers you will not be able to service any ships.\n"
             + "Whenever you fail a stage you can retry the stage by continuing with a penalty or by completely restarting the level.\n";
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