using System.Collections.Generic;
using UnityEngine;

public class OptionalArea : MonoBehaviour
{
    public Transform stackPrefab;

    public Game game;

    public List<Area> connected;


    private bool locked;

    private double price { get; set; }


    private void Update()
    {
        BuyArea();
    }

    private void BuyArea()
    {
        if (!(game.currentState is UpgradeState)) return;
        if (((UpgradeState) game.currentState).Buy(price))
        {
            Instantiate(stackPrefab, transform.position, transform.rotation).GetComponent<Area>().connected =
                connected;

            Destroy(gameObject);
        }
        else
            print("You don;t have enough money");
    }
}