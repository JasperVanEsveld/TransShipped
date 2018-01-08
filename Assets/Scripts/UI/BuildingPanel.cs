using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPanel : MonoBehaviour {
    private Game game;
    public Transform prefab;

    public Text text5, text1, text2, text3, text4;
    Button buttonObject;


    private void Start()
    {
        text1 = transform.Find("Text1").gameObject.GetComponent<Text>();
        text2 = transform.Find("Text2").gameObject.GetComponent<Text>();
        text3 = transform.Find("Text3").gameObject.GetComponent<Text>();
        text4 = transform.Find("Text4").gameObject.GetComponent<Text>();
        buttonObject = transform.Find("PurchaseButton").GetComponent<Button>();

    }

    public void Select(OptionalArea go, string objectname, string attribute1, string attribute2, string attribute3, string attribute4, string buttontext)
    {
        buttonObject.onClick.RemoveAllListeners();
        text1.text = attribute1;
        text2.text = attribute2;
        text3.text = attribute3;
        text4.text = attribute4;
        text5.text = objectname;
        buttonObject.gameObject.SetActive(true);
        buttonObject.onClick.AddListener(go.Buy);
        buttonObject.onClick.AddListener(Bought);
    }
    
    public void Select(Area go, string objectname, string attribute1, string attribute2, string attribute3, string attribute4, string buttontext)
    {
        text1.text = attribute1;
        text2.text = attribute2;
        text3.text = attribute3;
        text4.text = attribute4;
        text5.text = objectname;
    }

    public void Bought()
    {
        buttonObject.gameObject.SetActive(false);
    }

    private List<Transform> buttons = new List<Transform>();
	// Use this for initialization
    private void Awake () {
		game = FindObjectOfType<Game>();
        CreateButtons();
	}

    public void CreateButtons(){
        foreach(Transform button in buttons){
            Destroy(button.gameObject);
        }
        buttons.Clear();
        float x = 85f;
        foreach(OptionalArea area in game.optionalAreas){
            Transform obj = Instantiate(prefab);
            obj.SetParent(transform, false);
            buttons.Add(obj);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x,0);
            x += 170f;
            obj.GetComponent<Button>().onClick.AddListener(area.BuyArea);
            obj.GetChild(0).GetComponent<Text>().text = "Buy " + area.areaName;
        }
    }

    public void beginGame()
    {
        game.ChangeState(new OperationState(game));
    }
}
