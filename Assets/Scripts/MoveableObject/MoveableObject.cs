using System.Collections.Generic;
using UnityEngine;

public abstract class MoveableObject : MonoBehaviour
{
    // HOW TO USE:
    // for all vehicles (child class of this class), call Init() in their Start();
    // for all vehicles (child class of this class), call MovementUpdate() in their Update(); It will take care of all the movement thinggy
    // For ship, invoke MOEnterTerminal(Vector3 i_dest) and MOLeaveTerminal() to... have it enter and leave. 
    // The i_dest should be the position of a parking spot for the ship, the east one of the west one.
    // You can add a sth in the end of MOLeaveTerminal() to have the script destroy the GameObject after leaving the screen.
    // For ground vehicle, just invoke PushNewDest(Vector3 i_dest) to have it move to that position. (the position has to on the ground, obviously)
    // You can utilize MOIsObjectMoving() and MOIsAtTheThisPos(Vector3 i_pos) to help you with debugging, but ideally you should not need to use them anymore.


    // CALL THIS IN Update() IN WHICHEVER CHILD CLASS YOU ARE USING
    // It will takes care of all the movement stuff so you would not need to worry about them in child class anymore.
    protected void MOMovementUpdate()
    {
        transform.position = CalcPosForNextFrame();
    }

    /// <summary>
    /// Call this in Start() first in whichever child calss you are using
    /// </summary>
    /// <param name="i_initPos">Initial position</param>
    /// <param name="i_speed">Speed</param>
    /// <param name="i_isAtSeaLevel">Is this at sea level or not</param>
    protected void MOInit(Vector3 i_initPos, float i_speed, bool i_isAtSeaLevel)
    {
        movementQueue = new Queue<Vector3>();
        transform.position = new Vector3(i_initPos.x, height_, i_initPos.z);
        lastPos_ = transform.position;
        speed = i_speed;
        isSeaVehicle_ = i_isAtSeaLevel;
        if (isSeaVehicle_)
            height_ = -2.0f;
        else
            height_ = 0.0f;
    }

    // If the object is not moving/ does not have anywhere to go, return true
    public bool MOIsObjectMoving()
    {
        return movementQueue.Count != 0;
    }

    // For ground vehicles, tell to object to move to this place when it's done with wtever it is doing
    public void MOPushDestination(Vector3 i_dest)
    {
        if (MOIsAtTheThisPos(i_dest))
            return;

        int desRegion = GetRegion_(i_dest);
        int curRegion = GetRegion_(lastPos_);
//        Debug.Log(i_dest);
//        
//        Debug.Log(lastPos_);
        //Debug.Log("desRegion: " + desRegion + " curRegion: " + curRegion);
        if (curRegion == desRegion)
        {
            movementQueue.Enqueue(new Vector3(lastPos_.x, height_, i_dest.z));
            movementQueue.Enqueue(i_dest);
            lastPos_ = i_dest;
        }
        else
        {
            if (desRegion == 1)
            {
                movementQueue.Enqueue(new Vector3(lastPos_.x, height_, i_dest.z));
                movementQueue.Enqueue(i_dest);
                lastPos_ = i_dest;
            }
            else if (desRegion == 0 && curRegion == 2 || desRegion == 2 && curRegion == 0)
            {
                movementQueue.Enqueue(new Vector3(lastPos_.x, height_, i_dest.z));
                movementQueue.Enqueue(i_dest);
                lastPos_ = i_dest;
            }
            else if (desRegion == 4)
            {
                movementQueue.Enqueue(new Vector3(lastPos_.x, height_, i_dest.z));
                movementQueue.Enqueue(i_dest);
                lastPos_ = i_dest;
            }
            else
            {
                movementQueue.Enqueue(new Vector3(i_dest.x, height_, lastPos_.z));
                movementQueue.Enqueue(i_dest);
                lastPos_ = i_dest;
            }
        }
    }

    // Check if the object is at this place
    public bool MOIsAtTheThisPos(Vector3 i_pos)
    {
        Vector3 cur = transform.position;
        cur.y = height_;
        Vector3 des = i_pos;
        des.y = height_;

        float dis = Vector3.Distance(cur, des);
        return dis <= speed * Time.deltaTime;
    }

    // For ships, call this to have it enter terminal.
    // args: the position of the terminal area
    public void MOShipEnterTerminal(Vector3 dest)
    {
        int desRegion = GetRegion_(dest);
        if (desRegion == 5)
        {
            Vector3 intPoint2 = new Vector3(dest.x, height_, 36.0f);
            Vector3 intPoint1 = new Vector3(80.0f, height_, 36.0f);
            movementQueue.Enqueue(intPoint1);
            movementQueue.Enqueue(intPoint2);
            movementQueue.Enqueue(dest);
        }
        else if (desRegion == 3)
        {
            Vector3 intPoint2 = new Vector3(dest.x, height_, -36.0f);
            Vector3 intPoint1 = new Vector3(80.0f, height_, -36.0f);
            movementQueue.Enqueue(intPoint1);
            movementQueue.Enqueue(intPoint2);
            movementQueue.Enqueue(dest);
        }
    }

    // For ships, call this to have it leave terminal.
    public void MOShipLeaveTerminal()
    {
        int curRegion = GetRegion_(transform.position);
        if (curRegion == 5)
        {
            Vector3 intPoint1 = new Vector3(transform.position.x, height_, 36.0f);
            Vector3 intPoint2 = new Vector3(80.0f, height_, 36.0f);
            movementQueue.Enqueue(intPoint1);
            movementQueue.Enqueue(intPoint2);
        }
        else if (curRegion == 3)
        {
            Vector3 intPoint1 = new Vector3(transform.position.x, height_, -36.0f);
            Vector3 intPoint2 = new Vector3(80.0f, height_, -36.0f);
            movementQueue.Enqueue(intPoint1);
            movementQueue.Enqueue(intPoint2);
        }

        // TODO: Delete the parent GameObject
    }


    // ------------------- You should not need to worry about these below -----------------------//
    protected Queue<Vector3> movementQueue;

    private Vector3 lastPos_;
    private float speed;
    private float height_;
    private bool isSeaVehicle_;

    // Check if the object has finished the current movement task
    private bool IsAtTheCurrentDest_()
    {
        if (!MOIsObjectMoving()) return true;
        Vector3 cur = transform.position;
        cur.y = height_;
        Vector3 des = movementQueue.Peek();
        des.y = height_;

        float dis = Vector3.Distance(cur, des);
        return dis <= speed * Time.deltaTime;
    }

    // Return a pos that the object is supposed to be for the next frame
    private Vector3 CalcPosForNextFrame()
    {
        if (!MOIsObjectMoving())
            return transform.position;

        if (IsAtTheCurrentDest_())
        {
            movementQueue.Dequeue();
            if (!MOIsObjectMoving())
                return transform.position;
        }

        float step = speed * Time.deltaTime;
        Vector3 cur = transform.position;
        cur.y = height_;
        Vector3 des = movementQueue.Peek();
        des.y = height_;
        return Vector3.MoveTowards(cur, des, step);
    }


    private static int GetRegion_(Vector3 i_vec)
    {
        float zp = 12;
        float zn = -12;
        float xs = 30;
        float xc = 1;
        float x = i_vec.x;
        float z = i_vec.z;

        if (z <= zn)
            if (x <= xc)
                return 0;
            else if (x >= xs)
                return 6;
            else
                return 3;
        if (z >= zp)
            if (x <= xc)
                return 2;
            else if (x >= xs)
                return 8;
            else
                return 5;
        if (x <= xc)
            return 1;
        if (x >= xs)
            return 7;
        return 4;
    }
}