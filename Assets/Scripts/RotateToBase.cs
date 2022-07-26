using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// You should make this public to call functions Rotate from other scripts



public class RotateToBase : MonoBehaviour
{
    public GameObject myMainCamera_GO;                                          // Main Camera Keeps SafePositionsDictionary

    private SafePositions mySafePositions_Script;                               // acces to Safe Positions Dictionary
    private GameObject ball_GO;
    private Vector3 safeSphere_V3;                                              // keeps safe Sphere Rotation coordinates
    private Vector3 safeBall_V3;                                                // keeps safe Ball Position coordinates

    private Rigidbody ball_Rb;                                                  // use Rb to stop ball Rotation and Velocity when move to safe positions
    private Transform ball_Transform;

    public float rotateSpeed = 150f;
    public bool rotatingOn = false;

    public void Start()
    {
        mySafePositions_Script = myMainCamera_GO.GetComponent<SafePositions>();

        safeSphere_V3 = mySafePositions_Script.SafeSphereRotation;
        Debug.Log(safeSphere_V3);

        safeBall_V3 = mySafePositions_Script.SafeBallPosition;
        Debug.Log(safeBall_V3);

        StartCoroutine(CoroutineFindBallPosAndRb());                // find the ball RigidBody and Transform Components

    }
/*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotatingOn = true;
        }
        if (rotatingOn)
        {
            MovementProcess();              // good
            MoveBallToPos();                // not good !!!! // better to move ball using coroutine and wait until rotatingOn = false;
        }
        if (transform.rotation == Quaternion.Euler(safeSphere_V3))
        {
            rotatingOn = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mySafePositions_Script.RemoveOneFromKey();
            TakeSafeRotAndPos();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mySafePositions_Script.AddOneToKey();
            TakeSafeRotAndPos();
        }
    }
*/
    public void TakeSafeRotAndPos()
    {
        safeSphere_V3 = mySafePositions_Script.SafeSphereRotation;
        safeBall_V3 = mySafePositions_Script.SafeBallPosition;
        Debug.Log("New safe Rot is " + safeSphere_V3 + " and Pos added " + safeBall_V3);
    }

    public void MovementProcess()
    {
        if (rotatingOn)
        {
            var step = rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation.normalized, Quaternion.Euler(safeSphere_V3), step);
        }
    }

    void MoveBallToPos()
    {
        // disable rotation, moving and etc.
        ball_Rb.velocity = Vector3.zero;
        ball_Rb.angularVelocity = Vector3.zero;
        // then move to safe position
        ball_GO.transform.position = safeBall_V3;
    }

    IEnumerator CoroutineFindBallPosAndRb()
    {
        while (ball_GO == null)
        {
            print("The ball RigidBody haven't founded yet");
            ball_GO = GameObject.FindGameObjectWithTag("Ball");
            yield return new WaitForSeconds(1f);
        }
        if (ball_GO != null)
        {
            ball_Rb = ball_GO.GetComponent<Rigidbody>();
            ball_Transform = ball_GO.GetComponent<Transform>();
            print("The ball position and Rb was founded");
        }
        yield return null;
    }

}