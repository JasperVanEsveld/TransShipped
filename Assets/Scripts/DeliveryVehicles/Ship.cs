using UnityEngine;
using Random = System.Random;

public class Ship : DeliveryVehicle
{
    public Transform monoContainerPrefab;
    public ShipArea area;
    private int j, k;
    private Vector3 spawnPos = new Vector3(100.0f, -1.0f, 40.0f);
    private Vector3 spawnScale = new Vector3(20, 4, 2);
    private bool moveAxisOrder;


    public void SetSize(float i_size)
    {
        Vector3 temp = transform.localScale;
        temp.x = i_size;
        // So that the height does not change that much
        temp.y = i_size / 5.0f;
        temp.z = i_size / 10.0f;
        transform.localScale = temp;
    }

    // I can't figure out why cant I pass a Vector3 into this function
    // so this is a bit tricky to do....
    public void SetTargetPos()
    {
    }

    // Use this for initialization
    private void Start()
    {
        j = 0;
        k = 0;
        var rnd = new Random();
        // TODO: This probably will relate to size
        var conCount = rnd.Next(10, 30);

        for (var i = 0; i < conCount; ++i)
        {
            var temp = Instantiate(monoContainerPrefab, transform.position, transform.rotation)
                .GetComponent<MonoContainer>();
            carrying.Add(temp);
        }

        transform.position = spawnPos;
        transform.localScale = spawnScale;

        height = -1.0f;
        spawnPos = new Vector3(100.0f, height, 40.0f);
        spawnScale = new Vector3(20, 4, 2);
        transform.position = spawnPos;
        transform.localScale = spawnScale;
    }

    private void Update()
    {
        if (!(area.game.currentState is OperationState)) return;
        if (j == 0) EnterTerminal();
        j++;
        if (movementQueue.Count != 0)
        {
            if (Vector3.Distance(movementQueue.Peek(), transform.position) < speed * Time.deltaTime)
                movementQueue.Dequeue();
            if (movementQueue.Count != 0)
                transform.position = getNextPos();
        }
        if (!isAtDest()) return;
        if (k == 0) area.OnVehicleEnter(this);
        k++;
    }
}