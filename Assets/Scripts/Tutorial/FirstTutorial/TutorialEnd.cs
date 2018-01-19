public class TutorialEnd : TutorialState {
    public TutorialEnd(Tutorial tutorial) : base(tutorial) {
        text =
            "";
        tut.gameObject.SetActive(false);
    }

    public override void BindAll() {
    }

    public override void UnBindAll() {
    }
}