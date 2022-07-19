using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallSoundManager : MonoBehaviour
{

    public float distanceToGround;                  // should find out right value

    public AudioSource rollingAudioSource;

    private bool ball_IsGrounded;
    private float ball_Speed;
    private Rigidbody ball_Rigidbody;

    static readonly AudioAdjusmentSettings k_GroundedRollingVolume = new AudioAdjusmentSettings(1f / 1.5f, 0f, 1f, 25f); // (1f / 1.5f, 0f, 1f, 25f); // (0.5f, 0f, 1f, 10f);
    static readonly AudioAdjusmentSettings k_GroundedRollingPitch = new AudioAdjusmentSettings(1f / 1.2f, 0.8f, 1.5f, 15f); // (1f / 1.2f, 0.5f, 2f, 15f); // (0.5f, 1f, 1.8f, 5f);

    const float k_AirborneRollingTargetVolume = 0f;
    const float k_AirborneRollingVolumeChangeRate = 15f;

    const float k_AirborneRollingTargetPitch = 2f;
    const float k_AirborneRollingPitchChangeRate = 15f;

    void Start()
    {
        ball_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Raycast used to detect collision to things
        // So I think it used to understand stages: if Ball Is Grounded or not
        ball_IsGrounded = Physics.Raycast(ball_Rigidbody.position, Vector3.down, distanceToGround);
        ball_Speed = ball_Rigidbody.velocity.magnitude / 2;

        if (GameData.Instance.onSound == true)
        {
            if (ball_IsGrounded)
            {
                rollingAudioSource.volume = AudioAdjusmentSettings.ClampAndInterpolate(rollingAudioSource.volume, ball_Speed, k_GroundedRollingVolume);
                rollingAudioSource.pitch = AudioAdjusmentSettings.ClampAndInterpolate(rollingAudioSource.pitch, ball_Speed, k_GroundedRollingPitch);
            }
            else
            {
                rollingAudioSource.volume = Mathf.Lerp(rollingAudioSource.volume, k_AirborneRollingTargetVolume, k_AirborneRollingVolumeChangeRate * Time.deltaTime);
                rollingAudioSource.pitch = Mathf.Lerp(rollingAudioSource.pitch, k_AirborneRollingTargetPitch, k_AirborneRollingPitchChangeRate * Time.deltaTime);
            }
        }
        else
        {
            StopBallMoveSound();
        }
    }

    public void StopBallMoveSound()
    {
        ball_IsGrounded = false;
        rollingAudioSource.pitch = 0f;
        rollingAudioSource.volume = 0f;
    }
}
