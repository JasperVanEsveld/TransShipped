using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButtonOnClick : MonoBehaviour {
    private Game game;

    public void Start() {
        game = FindObjectOfType<Game>();
    }

    public void ContinueSuccess() {
        if (game.stages.Count == 0)
            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
                game.ChangeState(new LevelEndState());
            else {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                game.ChangeState(new UpgradeState());
            }
        else {
            game.SetStage(game.stages.Dequeue());
            game.ChangeState(new UpgradeState());
        }
    }

    public void ContinueFail() {
        game.ChangeState(new UpgradeState());
    }

    public void Restart(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }
}