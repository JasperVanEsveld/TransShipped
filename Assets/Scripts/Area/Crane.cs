using System;
using UnityEngine;

public class Crane : MonoBehaviour
{
    const double upbound = 10.0; // Subjected to change
    double speed; // for upgrade
    double baseTime;
    private DateTime startTime;
    public MonoContainer container { private get; set; }
    public CraneArea craneArea;
    public bool reserved;
    private Area reservedBy;

    // Upgrade and stuff
    private const int maxLevel_ = 3;
    private int level_ = 0;
    static private int[] costOfNextUpgrade_ = new int[maxLevel_] { 5, 10, 20 };
    static private double[] speedAtEachLevel_ = new double[maxLevel_ + 1] { 0.1, 2.0, 4.0, 8.0 };
    public bool IsFullyUpgraded() { return (level_ < maxLevel_) ? false : true; }
    public bool CanUpgrade() { if (IsFullyUpgraded() || Game.instance.money < costOfNextUpgrade_[level_]) return false; else return true; }
    public bool Upgrade()
    {
        if (CanUpgrade())
        {
            Game.instance.money -= costOfNextUpgrade_[level_];
            speed = speedAtEachLevel_[level_ + 1];
            ++level_;
            //Debug.Log("Money:"+ Game.instance.money + ", Level:"+level_+", Speed:"+speed);
            return true;
        }
        else return false;
    }
    // End of Upgrade and stuff

    private void Start()
    {
        speed = 1;
        baseTime = upbound - speed;
    }

    private void Awake()
    {
        container = null;
    }

    /// <summary>
    /// Check if the crane is available<br/>
    /// Return true if it is available and not reserved or reserved by you<br/>
    /// Otherwise return false
    /// </summary>
    /// <returns>Available or not</returns>
    public bool IsReady(Area origin)
    {

        return IsReady() || (container == null && reserved && reservedBy.Equals(origin));
    }

    /// <summary>
    /// Check if the crane is available<br/>
    /// Return true if it is available<br/>
    /// Otherwise return false
    /// </summary>
    /// <returns>Available or not</returns>
    public bool IsReady()
    {
        return container == null && !reserved;
    }

    public bool IsReservedBy(Area reference)
    {
        return reserved && reservedBy.Equals(reference);
    }
    
    /// <summary>
    /// Reserves this crane if possible
    /// </summary>
    /// <param name="origin"></param>
    /// <returns></returns>
    public bool ReserveCrane(Area origin){
        if(!reserved){
            reservedBy = origin;
            reserved = true;
            return true;
        } else{
            return false;
        }
    }


    // TODO: Associate this with sth in the GUI
    private void OnMouseDown()
    {
        Upgrade();
    }

    public bool AddContainer(MonoContainer monoContainer)
    {
        if (!IsReady(monoContainer.movement.originArea)) {
            return false;
        }
        reserved = false;
        container = monoContainer;
        container.transform.SetParent(transform);
        container.transform.position = transform.position;
        startTime = DateTime.Now;
        return true;
    }

    private void Update()
    {
        baseTime = upbound - speed;
        if (!(Game.instance.currentState is OperationState)) return;
        if (container != null && DateTime.Now.Subtract(startTime).TotalSeconds >= baseTime) {
            craneArea.MoveToNext(container);
        }
        else if (IsReady()){
            craneArea.AreaAvailable();
        }
    }
}