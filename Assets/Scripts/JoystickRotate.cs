using UnityEngine;

public class JoystickRotate : MonoBehaviour
{
    public float rotateSpeed = 50f;

    public Joystick joystick;
    public Joystick joystick2;

    public bool isEnabled_Joy;

    [SerializeField] private float zInput; // rotate to player and from player 
    [SerializeField] private float xInput;  // 
    [SerializeField] private float yInput; // rotate to the left and right 

    void Update()
    {
        if (isEnabled_Joy)
        {
            zInput = joystick.Vertical;
            yInput = joystick.Horizontal;
            xInput = joystick2.Horizontal;

            /* shoud change global axes to local after each Rotate */
            transform.Rotate(Vector3.forward, Time.deltaTime * rotateSpeed * zInput, Space.World);
            transform.Rotate(Vector3.right, Time.deltaTime * rotateSpeed * yInput, Space.World);
            transform.Rotate(Vector3.down, Time.deltaTime * rotateSpeed * xInput, Space.World);
        }
    }

    public void DisableJoyRotate()
    {
        isEnabled_Joy = false;
        zInput = 0f;
        yInput = 0f;
        xInput = 0f;
        print("input turn to Zero");
    }

    public void EnableJoyRotate()
    {
        isEnabled_Joy = true;
    }

}
