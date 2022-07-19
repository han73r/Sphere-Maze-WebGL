using UnityEngine;

public class MenuSphereRotation : MonoBehaviour
{
    float rotateSpeed = 10f;
    float moveSpeed = 0.03f;
    float moveLimit = 0.025f;            // limit to move up and down

    void Update()
    {
        Rotate();
        Move();
    }

    void FixedUpdate()
    {
        if (transform.position.y > moveLimit)
        {
            moveSpeed = -moveSpeed;
        }
        else if (transform.position.y < -moveLimit)
        {
            moveSpeed = -moveSpeed;
        }
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }

    void Move()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed); 
    }
}
