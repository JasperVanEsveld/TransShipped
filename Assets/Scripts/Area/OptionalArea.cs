using System.Collections.Generic;
using UnityEngine;


public class OptionalArea : HighlightAble
{
    public delegate void OpAreaListener(OptionalArea source);
    public static event OpAreaListener MouseDownEvent;
    public static event OpAreaListener AreaBought;
    public static event OpAreaListener NotEnough;

    public List<Area> connected;

    public string areaName;

    private GameObject stackPrefab;

    public double price = 10;
    public int capacity { private set; get; }

    private int i;

    private Color originColor;
    private readonly Color stackColor = new Color32(0xC0, 0xC0, 0xC0, 0xFF);
    public BuildingPanel buildingPanel;
    public string attribute;

    private void Start()
    {
        buildingPanel = GameObject.Find("BuildingPanel").GetComponent<BuildingPanel>();
        stackPrefab = Resources.Load("Areas/Stack") as GameObject;
        GetComponent<Renderer>().material.color = price <= Game.money ? Color.green : Color.red;
        Game.RegisterArea(this);
        capacity = 5 * (int) ((transform.lossyScale.x - 2) / 2) * (int) (transform.lossyScale.z - 2);
        InitHighlight();
    }

    public void BuyStack()
    {
        if (!(Game.instance.currentState is UpgradeState)) return;
        if (UpgradeState.Buy(price))
        {
            Game.DeregisterArea(this);
            Game.DeregisterHighlight(this);
            Vector3 sizeTemp = transform.localScale;
            var stack = Instantiate(stackPrefab, transform.position, transform.rotation).GetComponent<Stack>();
            stack.transform.localScale = sizeTemp;
            stack.max = capacity;
            foreach (var connectArea in connected)
            {
                stack.Connect(connectArea);
            }

            Destroy(gameObject);
            if(AreaBought != null){
                AreaBought.Invoke(this);
            }
        }
        else if (i == 0)
        {
            if(NotEnough != null){
                NotEnough.Invoke(this);
            }
        }

        i++;
    }

    private void OnMouseEnter()
    {
        if (!(Game.instance.currentState is UpgradeState)) return;
        GetComponent<Renderer>().material.color = stackColor;
        Game.RemoveHighlights();
        this.Highlight(true);
    }

    private void OnMouseExit()
    {
        if (!(Game.instance.currentState is UpgradeState)) return;
        GetComponent<Renderer>().material.color = originColor;
        this.Highlight(false);
    }

    private void OnMouseDown()
    {
        if(MouseDownEvent != null){
            MouseDownEvent.Invoke(this);
        }
        buildingPanel.SelectOptionalArea(this, areaName, attribute);
        Game.ForceRemoveHighlights();
        this.Highlight(true);
        lastClicked = true;
    }

    private void Update()
    {
        if (!(Game.instance.currentState is UpgradeState))
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
            originColor = price <= Game.money ? Color.green : Color.red;
        }
    }
}