using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public ParticleSystem confetti_PS_1R;
    public ParticleSystem confetti_PS_1L;
    public ParticleSystem confetti_PS_2;
    public ParticleSystem confetti_PS_3;
    public ParticleSystem confetti_PS_4;

    public AudioSource confettiAudioSource1;
    public AudioSource confettiAudioSource2;
    public AudioSource confettiAudioSource3;

    // public SafePositions mySafePositions_Script;

    private int firstSafePosition = 1;
    private int secondSafePosition = 2;
    private int winSafePosition = 3;

    private string myKeyToDict;

    public void PlayParticle(int step)
    {
        // myKeyToDict = mySafePositions_Script.KeyToDict;
        if (step == firstSafePosition)
        {
            confetti_PS_1L.Play();
            confetti_PS_1R.Play();
            confetti_PS_2.Play();
            if (GameData.Instance.onSound == true)                  // check Sound off/on status
            {
                confettiAudioSource1.Play(0);
                confettiAudioSource2.PlayDelayed(0.1f);
                confettiAudioSource3.PlayDelayed(0.2f);
            }
        }
        else if (step == secondSafePosition)
        {
            confetti_PS_3.Play();
            confetti_PS_4.Play();
            if (GameData.Instance.onSound == true)                  // check Sound off/on status
            {
                confettiAudioSource1.Play(0);
                confettiAudioSource2.PlayDelayed(0.1f);
            }
        }
        else if (step == winSafePosition)
        {
            confetti_PS_1L.Play();
            confetti_PS_1R.Play();
            confetti_PS_2.Play();
            confetti_PS_3.Play();
            confetti_PS_4.Play();
            if (GameData.Instance.onSound == true)                  // check Sound off/on status
            {
                confettiAudioSource1.Play(0);
                confettiAudioSource2.PlayDelayed(0.1f);
                confettiAudioSource3.PlayDelayed(0.2f);
            }
        }
        else
        {
            print("None particle system is requered");
        }
    }

}
