using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPanel : MonoBehaviour {
    public Transform prefab;

    public GameObject selected;
    private Text text5, text1, text2, text3, text4, buttonText;
    Button buttonObject;


    private void Start()
    {
        text1 = transform.Find("Text1").gameObject.GetComponent<Text>();
        text2 = transform.Find("Text2").gameObject.GetComponent<Text>();
        text3 = transform.Find("Text3").gameObject.GetComponent<Text>();
        text4 = transform.Find("Text4").gameObject.GetComponent<Text>();
        text5 = GameObject.Find("Text5").gameObject.GetComponent<Text>();
        buttonObject = transform.Find("PurchaseButton").GetComponent<Button>();
        buttonText = buttonObject.GetComponentInChildren<Text>();
    }

    public void SelectOptionalArea(OptionalArea go, string objectname, string attribute)
    {
        buttonObject.onClick.RemoveAllListeners();
        buttonText.text = "Purchase";
        text2.text = "Price for this area: " + go.price;
        text3.text = "Capacity for this area: " + go.capacity;
        text4.text = attribute;
        text5.text = objectname;
        buttonObject.gameObject.SetActive(true);
        buttonObject.onClick.AddListener(go.BuyStack);
        buttonObject.onClick.AddListener(Bought);
    }

    public void SelectCraneArea(CraneArea go, string objectname, string attribute)
    {
        buttonObject.onClick.RemoveAllListeners();
        buttonText.text = "Buy a new Crane";
        text2.text = "Price for one crane: " + go.priceForOneCrane;
        {
            text3.text = "Currently there is " + go.cranes.Count + " crane";
            if (go.cranes.Count > 1) text3.text += "s";
        }
        text4.text = attribute;
        text5.text = objectname;
        buttonObject.gameObject.SetActive(true);
        buttonObject.onClick.AddListener(go.BuyCrane);
        buttonObject.onClick.AddListener(Bought);
    }

    private void Bought()
    {
        buttonObject.gameObject.SetActive(false);
        text1.text = "";
        text2.text = "";
        text3.text = "";
        text4.text = "";
        text5.text = "";
        buttonText.text = "";
    }


    int buttonCount = 0;
    List<Transform> buttons = new List<Transform>();

    public void beginGame()
    {
        Game.instance.ChangeState(new OperationState());
    }
}