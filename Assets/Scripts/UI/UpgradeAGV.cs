using Assets.Scripts.MoveableObject;
using UnityEngine;

public class UpgradeAGV : MonoBehaviour
{
    public void Pressed()
    {
        Vehicle.Upgrade();
    }
}