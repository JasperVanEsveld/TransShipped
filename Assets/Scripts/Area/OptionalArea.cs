using System.Collections.Generic;
using UnityEngine;

public class OptionalArea : MonoBehaviour
{
    private Game game { get; set; }

    public List<Area> connected;

    private GameObject stackPrefab;

    public double price = 10;

    private bool locked;

    private void Start()
    {
        game = (Game) FindObjectOfType(typeof(Game));
        stackPrefab = Resources.Load("Areas/Stack") as GameObject;
    }

    private void Update()
    {
        BuyArea();
    }

    private void BuyArea()
    {
        if (!(game.currentState is UpgradeState)) return;
        if (((UpgradeState) game.currentState).Buy(price))
        {
            var stack = Instantiate(stackPrefab, transform.position, transform.rotation).GetComponent<Area>();
            foreach (var connectArea in connected)
            {
                stack.Connect(connectArea);
            }
            Destroy(gameObject);
        }
        else
            print("You don;t have enough money");
    }
}