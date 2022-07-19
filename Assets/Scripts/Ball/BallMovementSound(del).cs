using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BallMovementSound : MonoBehaviour
{/*
    AudioSource AudioSource;
    [SerializeField] AudioClip ballMoveSound;
    GameObject ball;
    Rigidbody ballRb;

    void Start()
    {
        AudioSource.clip = ballMoveSound;
        AudioSource.enabled = true;
        StartCoroutine(CoroutineFindBallPosAndRb());
    }

    void FixedUpdate()
    {
        if (ballRb! && ballRb.velocity.magnitude >= 0.1f && AudioSource.isPlaying == false)
        {
            AudioSource.PlayOneShot(ballMoveSound, 0.7f);
            print("Play Ball Moving Sound");
        }
    }

    IEnumerator CoroutineFindBallPosAndRb()
    {
        while (ball == null)
        {
            ball = GameObject.FindGameObjectWithTag("Ball");
            yield return new WaitForSeconds(1f);
        }
        if (ball != null)
        {
            ballRb = ball.GetComponent<Rigidbody>();
        }
        yield return null;
    }
*/


    /*
    [SerializeField] float ballVelocity;
    [SerializeField] AudioSource ballSound;


    // should understand speed and different states: low spd // mid spd // fast spd // fast stop 
    void Start()
    {
        ballSound = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (ballVelocity > 0.5f)
        {
            ballSound.Play(0);
            print("Play");
        }
        else if (ballVelocity < 0.5f)
        {
            ballSound.Stop();
            print("Stop");
        }
    }*/
}
