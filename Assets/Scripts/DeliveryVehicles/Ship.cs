using System.Collections.Generic;
using UnityEngine;

public class Ship : DeliveryVehicle
{
    public ShipArea area;
    private int j, k;

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
        GenerateRandomContainers(10, 30);

        height = -1.0f;
        spawnPos = new Vector3(100.0f, height, 40.0f);
        spawnScale = new Vector3(20, 4, 2);

        Debug.Log(spawnPos);
        Debug.Log(transform.position);
        transform.position = spawnPos;
        Debug.Log(spawnPos);
        Debug.Log(transform.position);
        transform.localScale = spawnScale;


        List<ShipArea> areaList = GameObject.Find("Game").GetComponent<Game>().GetAreasOfType<ShipArea>();

        //TODO:get the first free area
        area = areaList[0];

        destPos = area.transform.position;
        destPos.y = height;
    }

    private void Update()
    {
        if (!(this.game.currentState is OperationState)) return;
        j++;
        if (movementQueue.Count != 0)
        {
            if (Vector3.Distance(movementQueue.Peek(), transform.position) < speed * Time.deltaTime)
                movementQueue.Dequeue();
            if (movementQueue.Count != 0)
                transform.position = getNextPos();
        }
       // if (!isAtDest()) return;
       // if (k == 0) area.OnVehicleEnter(this);
       // k++;
    }
}