public class ReadyState : TutorialState {
    public ReadyState(Tutorial tutorial) : base(tutorial) {
        text = "You could buy a better crane or more cranes later but sadly you don't have any money left.\n"
             + "Luckily you can make money in the operation stage!\n"
             + "Get ready and press begin stage to start.";
    }

    private void GameStarted() {
        tut.currentState = new GameStartedState(tut);
    }

    public override void BindAll() {
        BuildingPanel.GameStarted += GameStarted;
    }

    public override void UnBindAll() {
        BuildingPanel.GameStarted -= GameStarted;
    }
}