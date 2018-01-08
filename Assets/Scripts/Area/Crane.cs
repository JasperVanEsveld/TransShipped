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
    static private Game game;

    // Upgrade and stuff
    private const int maxLevel_ = 3;
    private int level_ = 0;
    static private int[] costOfNextUpgrade_ = new int[maxLevel_] { 5, 10, 20 };
    static private double[] speedAtEachLevel_ = new double[maxLevel_ + 1] { 1.0, 2.0, 4.0, 6.0 };
    public bool IsFullyUpgraded() { return (level_ < maxLevel_) ? true : false; }
    public bool CanUpgrade() { if (IsFullyUpgraded() || game.money < costOfNextUpgrade_[level_]) return false; else return true; }
    public bool Upgrade()
    {
        if (CanUpgrade())
        {
            game.money -= costOfNextUpgrade_[level_];
            speed = speedAtEachLevel_[level_ + 1];
            ++level_;
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
        game = (Game) FindObjectOfType(typeof(Game));
    }

    /// <summary>
    /// Check if the crane is available<br/>
    /// Return true if it is available<br/>
    /// Otherwise return false
    /// </summary>
    /// <returns>Available or not</returns>
    public bool IsAvailable()
    {
        return container == null;
    }

    public bool AddContainer(MonoContainer monoContainer)
    {
        if (!IsAvailable())
        {
            if (container.movement.originArea != monoContainer.movement.originArea
                && monoContainer.movement.originArea is Road)
            {
//                    print("Holding container from: " + container.movement.originArea);
//                    print("New container from: " + monoContainer.movement.originArea);
                Area previous = container.movement.originArea;
                container.movement.originArea = craneArea;
                if (!previous.AddContainer(container)) return false;
                craneArea.RemoveContainer(container);
            }
            else return false;
        }

//            print("Received container from: " + monoContainer.movement.originArea);
        container = monoContainer;
        container.transform.SetParent(transform);
        container.transform.position = transform.position;
        startTime = DateTime.Now;
        return true;
    }

    private void Update()
    {
        baseTime = upbound - speed;
        if (!(game.currentState is OperationState)) return;
        if (!IsAvailable() && DateTime.Now.Subtract(startTime).TotalSeconds >= baseTime)
            craneArea.MoveToNext(container);
        else if (IsAvailable())
            craneArea.AreaAvailable();
    }
}