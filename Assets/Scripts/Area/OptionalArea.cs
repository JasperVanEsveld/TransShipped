using System.Collections.Generic;
using UnityEngine;

public class OptionalArea : MonoBehaviour
{
    public Transform stackPrefab;

    public Game game;

    public List<Area> connected;

    public double price = 10;

    private bool locked;

    private void Update()
    {
        BuyArea();
    }

    private void BuyArea()
    {
        if (!(game.currentState is UpgradeState)) return;
        if (((UpgradeState) game.currentState).Buy(price))
        {
            var newArea = Instantiate(stackPrefab, transform.position, transform.rotation).GetComponent<Area>();
            foreach (var connectArea in connected)
            {
                newArea.Connect(connectArea);
            }
            Destroy(gameObject);
        }
        else
            print("You don;t have enough money");
    }
}