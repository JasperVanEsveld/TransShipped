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
            game.ChangeState(new LevelEndState(game));
        } else{
            game.ChangeState(new UpgradeState(game));
        }
    }
    public void ContinueFail()
    {
        game.ChangeState(new UpgradeState(game));
    }

    public void Restart(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}