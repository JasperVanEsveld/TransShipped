using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleShip : DeliveryVehicle
{
    

    // I can't figure out why cant I pass a Vector3 into this function
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
    void Start ()
    {
        height_ = -1.0f;
        destPos_ = new Vector3(17.0f, height_, 17.0f);
        spawnPos_ = new Vector3(100.0f, height_, 40.0f);
        spawnScale_ = new Vector3(20, 4, 2);
        carrying = new List<MonoContainer>();
        System.Random rnd = new System.Random();
        // TODO: This probably will relate to size
        int conCount = rnd.Next(10, 30);

        for (int i = 0; i < conCount; ++i)
            carrying.Add(new MonoContainer(new Container(true, 1), null));

        transform.position = spawnPos_;
        transform.localScale = spawnScale_;
        targetPos_ = transform.position;
    }

    private void Update()
    {
        if (movementQueue_.Count != 0)
        {
            if (Vector3.Distance(movementQueue_.Peek(), transform.position) < speed_ * Time.deltaTime)
            {
                movementQueue_.Dequeue();
            }
            if (movementQueue_.Count != 0)
                transform.position = getNextPos();
        }

        // If at port
        
        if (isAtDest())
        {
            Debug.Log("At Port");
        }
    }

}
