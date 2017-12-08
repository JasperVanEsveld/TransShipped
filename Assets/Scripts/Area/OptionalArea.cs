using System.Collections.Generic;
using UnityEngine;

public class OptionalArea : MonoBehaviour
{
    private Game game { get; set; }

    public List<Area> connected;

    private GameObject stackPrefab;

    public double price = 10;

    private bool locked;

    private int i = 0;

    private void Start()
    {
        game = (Game) FindObjectOfType(typeof(Game));
        stackPrefab = Resources.Load("Areas/Stack") as GameObject;
    }

    //todo binding BuyArea() function to a button instead of calling it in the Update()

    private void Update()
    {
        BuyArea();
    }

    private void BuyArea()
    {
        if (!(game.currentState is UpgradeState)) return;
        if (((UpgradeState) game.currentState).Buy(price))
        {
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
}