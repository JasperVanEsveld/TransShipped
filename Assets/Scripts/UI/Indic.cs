using UnityEngine;
using UnityEngine.UI;

public class Indic : MonoBehaviour {
    private double lastMoney = 0.0f;

    public void ShowLatestChange(double i_amount) {
        if (i_amount > 0.0) {
            GetComponent<Text>().text = "+" + i_amount;
            GetComponent<Text>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        } else {
            GetComponent<Text>().text = "" + i_amount;
            GetComponent<Text>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    private void Update() {
        float speedOfDecay = 0.01f;
        Color c = GetComponent<Text>().color;
        //Debug.Log(c.a);
        c.a = c.a - speedOfDecay;
        if (c.a <= 0.0f) c.a = 0.0f;
        GetComponent<Text>().color = c;
    }
}