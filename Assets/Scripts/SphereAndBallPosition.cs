using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAndBallPosition : MonoBehaviour
{
    public Vector3 sphereRot;
    public Vector3 ballPos;
    public SphereAndBallPosition(Vector3 newSphereRot, Vector3 newballPos)
    {
        sphereRot = newSphereRot;
        ballPos = newballPos;
    }
}
