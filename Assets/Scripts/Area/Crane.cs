using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Crane : MonoBehaviour
    {
        private const double upbound = 10;
        private double speed;
        public float baseTime;
        private DateTime startTime;
        public MonoContainer container { private get; set; }
        public CraneArea craneArea;
        private Game game;

        //    Felix ********************************
        private const int maxLevel = 3;
        private int level;
        private static readonly int[] costOfNextUpgrade = {5, 10, 20};
        private static readonly double[] speedAtEachLevel = {1, 2, 4, 6};

        private bool IsFullyUpgraded()
        {
            return level >= maxLevel;
        }

        private bool IsUpgradable()
        {
            return !IsFullyUpgraded() && !(game.money < costOfNextUpgrade[level]);
        }

        public bool Upgrade()
        {
            if (!(game.currentState is OperationState)) return false;
            if (!IsUpgradable()) return false;
            game.SetMoney(game.money - costOfNextUpgrade[level]);
            speed = speedAtEachLevel[++level];
            return true;
        }
        //**********************************

        private void Awake()
        {
            container = null;
            level = 0;
            speed = speedAtEachLevel[level];
            baseTime = (float) (upbound - speed);
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
            if (!(game.currentState is OperationState)) return;
            baseTime = (float) (upbound - speed);
            if (!IsAvailable() && DateTime.Now.Subtract(startTime).TotalSeconds >= baseTime)
                craneArea.MoveToNext(container);
            else if (IsAvailable())
                craneArea.AreaAvailable();
        }
    }
}