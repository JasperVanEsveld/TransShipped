using System.Collections.Generic;
using UnityEngine;

public class OptionalArea : MonoBehaviour
{
    private Game game { get; set; }

    public List<Area> connected;

    public string areaName;

    private GameObject stackPrefab;

    public double price = 10;

    private bool locked;

    private int i;

    private Color originColor;

    private readonly Color stackColor = new Color32(0xC0, 0xC0, 0xC0, 0xFF);

    private void Start()
    {
        game = (Game) FindObjectOfType(typeof(Game));
        stackPrefab = Resources.Load("Areas/Stack") as GameObject;
        GetComponent<Renderer>().material.color = price <= game.money ? Color.green : Color.red;
        game.RegisterArea(this);
    }

    [System.Obsolete("This function is meant for the building panel, now it is obsolete.")]
    public void BuyArea()
    {
        if (!(game.currentState is UpgradeState)) return;
        if (((UpgradeState) game.currentState).Buy(price))
        {
            game.optionalAreas.Remove(this);
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
        if (!(game.currentState is UpgradeState)) return;
        GetComponent<Renderer>().material.color = stackColor;
    }

    private void OnMouseExit()
    {
        if (!(game.currentState is UpgradeState)) return;
        GetComponent<Renderer>().material.color = originColor;
    }

    private void OnMouseDown()
    {
        if (!(game.currentState is UpgradeState)) return;
        if (((UpgradeState) game.currentState).Buy(price))
        {
            game.optionalAreas.Remove(this);
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
        if (!(game.currentState is UpgradeState))
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
            originColor = price <= game.money ? Color.green : Color.red;
        }
    }
}