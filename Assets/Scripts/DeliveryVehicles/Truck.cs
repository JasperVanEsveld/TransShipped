using UnityEngine;

public class Truck : DeliveryVehicle
{
    public TruckArea area;
    private int j, k;
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
        // TODO: This probably will relate to size

        transform.position = spawnPos;
        transform.localScale = spawnScale;

        height = 0.0f;
        spawnPos = new Vector3(20.0f, height, 50.0f);
        spawnScale = new Vector3(1, 1, 1);
        destPos = new Vector3(-13, 0, -37);
        transform.position = spawnPos;
        transform.localScale = spawnScale;
        
        GenerateRandomContainers(10, 30);
    }

    private void Update()
    {
        if (!(game.currentState is OperationState)) return;
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