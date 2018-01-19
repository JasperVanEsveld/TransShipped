public class TutorialFailed : TutorialState {
    public TutorialFailed(Tutorial tutorial) : base(tutorial) {
        text =
              "Your solution was not good enough. \n You can either restart the level or" 
            + " if you think you can still pass the stage (given a certain penality) you can do that as well.";
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