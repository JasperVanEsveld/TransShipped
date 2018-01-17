using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indic : MonoBehaviour {

    private double lastMoney = 0.0f;

    public void ShowLatestChange(double i_amount)
    {
        if (i_amount > 0.0)
        {
            GetComponent<Text>().text = "+" + i_amount;
            GetComponent<Text>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        }
        else
        {
            GetComponent<Text>().text = "" + i_amount;
            GetComponent<Text>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float speedOfDecay = 0.03f;
        Color c = GetComponent<Text>().color;
        //Debug.Log(c.a);
        c.a = c.a - speedOfDecay;
        if (c.a <= 0.0f) c.a = 0.0f;
        GetComponent<Text>().color = c;
    }
}
