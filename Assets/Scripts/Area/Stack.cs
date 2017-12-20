using System.Collections.Generic;
using UnityEngine;

public class Stack : Area
{
    public List<MonoContainer> containers = new List<MonoContainer>();

    private CapacitySlider script;

    public int max;

    public GameObject slider;
    GameObject sliderclone;
    public int Contains(Container container)
    {
        for (var i = 0; i < containers.Count; i++)
        {
            if (containers[i].container.Equals(container))
            {
                return i;
            }
        }
        return -1;
    }

    public void Update()
    {
        for (var i = containers.Count - 1; i >= 0; i--)
        {
            if (MoveToNext(containers[i]))
            {
                break;
            }
            var n = i % (max / 5);
            containers[i].transform.position = new Vector3(
                transform.position.x - transform.lossyScale.x / 2 + 2 + 2 * (int) (n / (transform.lossyScale.z - 2)),
                i * 5 / max,
                transform.position.z - transform.lossyScale.z / 2 + 1.5f + n % (transform.lossyScale.z - 2));
        }


    }

    public override bool AddContainer(MonoContainer monoContainer)
    {
        if (containers.Count >= max) return false;
        containers.Add(monoContainer);
        if (monoContainer.movement.TargetArea == this)
        {
            monoContainer.movement = null;
            
        }
        return true;
    }

    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        containers.Remove(monoContainer);
    }

    private void OnMouseEnter()
    {

        if ((game.currentState is OperationState))
        {


            sliderclone = Instantiate(slider, transform.position, Quaternion.identity);

            sliderclone.transform.SetParent(GameObject.Find("Canvas").transform, false);

            //sliderclone.transform.parent = GameObject.Find("Canvas").transform;
            CapacitySlider script = sliderclone.GetComponent<CapacitySlider>();
                script.target = this.transform;
            GetComponent<Renderer>().material.color = Color.cyan;
            script.ChangeSliderValue((float)containers.Count / max); 


        }
    }

    private void OnMouseDown()
    {

    }

    private void OnMouseExit()
    {
        if ((game.currentState is OperationState))
        {

            Destroy(sliderclone);
            GetComponent<Renderer>().material.color = Color.grey;
        }
    }
}

