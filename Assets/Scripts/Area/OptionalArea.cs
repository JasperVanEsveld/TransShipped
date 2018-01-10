using System.Collections.Generic;
using UnityEngine;

public class OptionalArea : MonoBehaviour
{
    public List<Area> connected;

    public string areaName;

    private GameObject stackPrefab;

    public double price = 10;

    private bool locked;

    private int i;

    private Color originColor;
    private readonly Color stackColor = new Color32(0xC0, 0xC0, 0xC0, 0xFF);
    public BuildingPanel buildingPanel;
    public string attribute1, attribute2, attribute3, attribute4, buttontext;

    private void Start()


    {

        buildingPanel = GameObject.Find("BuildingPanel").GetComponent<BuildingPanel>();


        stackPrefab = Resources.Load("Areas/Stack") as GameObject;
        GetComponent<Renderer>().material.color = price <= Game.instance.money ? Color.green : Color.red;
        Game.instance.RegisterArea(this);
    }

    [System.Obsolete("This function is meant for the building panel, now it is obsolete.")]
    public void BuyArea()
    {
        if (!(Game.instance.currentState is UpgradeState)) return;
        if (((UpgradeState) Game.instance.currentState).Buy(price))
        {
            Game.instance.optionalAreas.Remove(this);
            var stack = Instantiate(stackPrefab, transform.position, transform.rotation).GetComponent<Stack>();
            stack.max = 5 * (int) ((transform.lossyScale.x - 2) / 2) * (int) (transform.lossyScale.z - 2);
            foreach (var connectArea in connected)
            {
                stack.Connect(connectArea);
            }
            Destroy(gameObject);
        }
        else if (i == 0)
        {
            print("You don't have enough money");
        }
        i++;
    }

    private void OnMouseEnter()
    {
        if ((Game.instance.currentState is UpgradeState))
        {
                GetComponent<Renderer>().material.color = stackColor;
        }
    }

    private void OnMouseExit()
    {
        if (!(Game.instance.currentState is UpgradeState)) return;
        GetComponent<Renderer>().material.color = originColor;
    }

    private void OnMouseDown() {
        buildingPanel.Select(this, areaName, attribute1, attribute2, attribute3, attribute4, buttontext);
    }

    public void Buy()
    {
        if (!(Game.instance.currentState is UpgradeState)) return;
        if (((UpgradeState) Game.instance.currentState).Buy(price))
        {
            Game.instance.optionalAreas.Remove(this);
            var stack = Instantiate(stackPrefab, transform.position, transform.rotation).GetComponent<Stack>();
            stack.max = 5 * (int) ((transform.lossyScale.x - 2) / 2) * (int) (transform.lossyScale.z - 2);
            foreach (var connectArea in connected)
            {
                stack.Connect(connectArea);
            }
            Destroy(gameObject);
        }
        else if (i == 0)
        {
            print("You don't have enough moeny");
        }
        i++;
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
            originColor = price <= Game.instance.money ? Color.green : Color.red;
        }
    }
}