public abstract class TutorialState
{
    public string text;
    protected Tutorial tut;

    public TutorialState(Tutorial tutorial){
        tut = tutorial;
        BindAll();
    }

    public abstract void BindAll();

    public abstract void UnBindAll();
}