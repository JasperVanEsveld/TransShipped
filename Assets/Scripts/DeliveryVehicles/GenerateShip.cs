using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GenerateShip : MonoBehaviour
{
    private int shipCount_ = 0;
    public void generateShip()
    {
        if (shipCount_ == 5) return;
        int i = shipCount_;
       // Debug.Log("generateShip(" + i + ")");
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = "Ship_" + i;
        obj.AddComponent<Ship>();
        ++shipCount_;

        //return obj;
    }
}
