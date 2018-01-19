public class SecondTutorial : Tutorial
{
    public void Start() {
        currentState = new WelcomeSecondState(this);
        textBox.text = currentState.text;
    }
}