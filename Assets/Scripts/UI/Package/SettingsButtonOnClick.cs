using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButtonOnClick : MonoBehaviour
{
    private Game game;

    public void Start(){
        game = FindObjectOfType<Game>();
    }

    public void ContinueSuccess()
    {
        if(game.stages.Count == 0){
            game.ChangeState(new LevelEndState());
        } else{
            game.SetStage(game.stages.Dequeue());
            game.ChangeState(new UpgradeState());
        }
    }
    public void ContinueFail()
    {
        game.ChangeState(new UpgradeState());
    }

    public void Restart(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}