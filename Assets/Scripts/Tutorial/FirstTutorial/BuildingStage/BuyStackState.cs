public class BuyStackState : TutorialState {
    public BuyStackState(Tutorial tutorial) : base(tutorial) {
        text = "To now buy this area you click the purchase button";
    }

    private void GameStarted(){
        tut.currentState = new NoStackWarning(tut);
    }

    public void AreaBought(OptionalArea area) {
        tut.currentState = new ClickCraneState(tut);
    }

    public override void BindAll(){
        OptionalArea.AreaBought += AreaBought;
        BuildingPanel.GameStarted += GameStarted;
    }

    public override void UnBindAll(){
        OptionalArea.MouseDownEvent -= AreaBought;
        BuildingPanel.GameStarted -= GameStarted;
    }
}