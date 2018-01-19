using UnityEngine;
using UnityEngine.UI;

public abstract class Tutorial : MonoBehaviour
{
    public Text textBox;
    private TutorialState state;
    public TutorialState currentState { 
        get{
            return state;
        }
        set{
            if(state != null){
                state.UnBindAll();
            }
            state = value;
            textBox.text = state.text;
        } 
    }

    void OnDestroy(){
        state.UnBindAll();
    }
}