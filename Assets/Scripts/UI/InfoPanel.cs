using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    private Game game;
    public Text targetText;
    public Text moneyText;

	// Use this for initialization
	void Start () {
        game = FindObjectOfType<Game>();
        game.moneyChangeEvent += new OnMoneyChanged(SetMoney);
        game.stageChangeEvent += new OnStageChanged(StageChanged);
        moneyText.text = "Money :" + game.money;

    }
	
	// Update is called once per frame
	void Update () {
    }

    public void SetMoney(double newMoney) {
        moneyText.text = "Money :" + newMoney;
    }

    public void StageChanged(Stage newStage) {
       targetText.text = "Target :" + newStage.moneyRequired;
    }
}
