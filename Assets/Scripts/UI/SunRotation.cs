using UnityEngine;

public class SunRotation : MonoBehaviour {
    private void Start() {
        transform.rotation = Quaternion.Euler(-70, 0, 0);
    }

    private void Update() {
        if (Game.instance.currentState is OperationState)
            transform.Rotate(Vector3.right * Time.deltaTime);
    }
}