using UnityEngine;

/* Rotate object X, Y and Z axes by QWASDE */

public class MazeRotate : MonoBehaviour
{
    public float rotateSpeed = 50f;
    private float zInput; // rotate to player and from player 
    private float xInput;  // 
    private float yInput; // rotate to the left and right 

    [SerializeField] float sphereAngleX, sphereAngleY, sphereAngleZ;
    [SerializeField] float sphereQuartX, sphereQuartY, sphereQuartZ, sphereQuartW;

    void FixedUpdate()
    {
        sphereAngleX = transform.rotation.eulerAngles.x;
        sphereAngleY = transform.rotation.eulerAngles.y;
        sphereAngleZ = transform.rotation.eulerAngles.z;
        sphereQuartX = transform.rotation.x;
        sphereQuartY = transform.rotation.y;
        sphereQuartZ = transform.rotation.z;
        sphereQuartW = transform.rotation.w;
        xInput = Input.GetAxis("RotateX");
        yInput = Input.GetAxis("RotateY");
        zInput = Input.GetAxis("RotateZ");

        /* Rotate at any axes */
        transform.Rotate(Vector3.down, Time.deltaTime * rotateSpeed * xInput, Space.World);
        transform.Rotate(Vector3.back, Time.deltaTime * rotateSpeed * yInput, Space.World);
        transform.Rotate(Vector3.right, Time.deltaTime * rotateSpeed * zInput, Space.World);
    }

    /* void Update()
     {
         if (Input.GetKeyDown(KeyCode.Alpha1))
         {
             RotateToPos();
         }
     }   

     // give this method the infromation which button was used
     void RotateToPos()
     {
        /* transform. (-0.275f, 161.162f, 57.582f, Space.World);*/

}