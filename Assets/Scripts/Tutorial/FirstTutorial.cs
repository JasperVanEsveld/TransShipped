public class FirstTutorial : Tutorial
{
    public void Start() {
        currentState = new ClickStackState(this);
        textBox.text = currentState.text;
    }
}