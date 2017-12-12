using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    private Text[] handle_;

	// Use this for initialization
	void Start () {
        handle_ = GetComponentsInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {

            
    }

    public void SetMoney(double i_money)
    {
        handle_[0].text = "Money :" + i_money;
    }

    public void SetScore(double i_score)
    {
        handle_[1].text = "Score :" + i_score;
    }
}
