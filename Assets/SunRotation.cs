using UnityEngine;

public class SunRotation : MonoBehaviour
{
    public float rotationSpeed;
    public float xAngle;
    public float yAngle;
    public float zAngle;
    private Game game;

    private void Start()
    {
        game = FindObjectOfType<Game>();
        transform.rotation = Quaternion.Euler(xAngle, yAngle, zAngle);
    }

    private void Update()
    {
        if (game.currentState is OperationState)
            transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
    }
}