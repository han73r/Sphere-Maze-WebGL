using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehave : MonoBehaviour
{
    Rigidbody ballRb;
    bool isSleep;


    void Awake()
    {
        ballRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }
}
