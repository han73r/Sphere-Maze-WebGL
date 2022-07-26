using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// indicates which object ball is collided with
public class BallCollides : MonoBehaviour
{
    private GameObject myMainCamera_GO;
    private SafePositions mySafePositions_Script;
    private ParticleManager myParticleManager_Script;

    private GameObject myMassCenter_GO;
    private RotateToBase myRotateToBase_Script;

    private GameObject myGameManager_GO;
    private GameManager myGameManager_Script;
    private TimeManager myTimeManager_Script;

    public BallSoundManager myBallSoundManager;

    private string collideSafePosName;
    private string previousSafePosName;

    public GameObject particle_GO;
    private Rigidbody ball_Rb;
    private ParticleSystem dust_PS;

    [SerializeField] private float ballSpeed;
    private float maxSpeedForDust = 1.5f;

    private bool isGrounded = false;
    [SerializeField] private float distanceToGround;

    private string firstSafePosition = "pos2";
    private string secondSafePosition = "pos9";
    private string winSafePosition = "pos18";

    private int step1 = 1;
    private int step2 = 2;
    private int step3 = 3;

    void Start()
    {
        myMainCamera_GO = GameObject.Find("Main Camera");
        mySafePositions_Script = myMainCamera_GO.GetComponent<SafePositions>();

        myMassCenter_GO = GameObject.Find("MassCenter");
        myRotateToBase_Script = myMassCenter_GO.GetComponent<RotateToBase>();

        myGameManager_GO = GameObject.Find("GameManager");
        myGameManager_Script = myGameManager_GO.GetComponent<GameManager>();
        myTimeManager_Script = myGameManager_GO.GetComponent<TimeManager>();
        myParticleManager_Script = myGameManager_GO.GetComponent<ParticleManager>();
    
        ball_Rb = GetComponent<Rigidbody>();

        myBallSoundManager = GetComponent<BallSoundManager>();

        dust_PS = particle_GO.GetComponent<ParticleSystem>();
        print("particle system is " + dust_PS);

    }

    void FixedUpdate()
    {

        // check pos under table!
        ballSpeed = ball_Rb.velocity.magnitude;

        isGrounded = Physics.Raycast(ball_Rb.position, Vector3.down, distanceToGround);

        if (isGrounded)
        {
            if (ballSpeed > maxSpeedForDust)
            {
                StartCoroutine(MakeDust());
            }
            else
            {
                StopCoroutine(MakeDust());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafePosition"))
        {
            if (collideSafePosName != null)
            {
                previousSafePosName = collideSafePosName;
            }

            collideSafePosName = other.name;

            if (collideSafePosName != previousSafePosName)
            {
                Debug.Log("The ball reached pos number " + collideSafePosName);
                // ADD Play Sound Here TOO
                ChangeDictKey();

                // Detect Important position (play particles and change time laps)
                if (collideSafePosName == firstSafePosition || collideSafePosName == secondSafePosition || collideSafePosName == winSafePosition)
                {
                    

                    // ADD Play Sound Here TOO

                    // play particle and show/save sector time, depends on collider name 
                    if (collideSafePosName == firstSafePosition)
                    {   
                        myParticleManager_Script.PlayParticle(step1);
                        myTimeManager_Script.ChangeColor_ShowBestSectorTime(step1);
                    }
                    else if (collideSafePosName == secondSafePosition)
                    {
                        myParticleManager_Script.PlayParticle(step2);
                        myTimeManager_Script.ChangeColor_ShowBestSectorTime(step2);
                    }
                    else if (collideSafePosName == winSafePosition)
                    {
                        myParticleManager_Script.PlayParticle(step3);
                        myTimeManager_Script.ChangeColor_ShowBestSectorTime(step3);
                        myGameManager_Script.WinGame();
                    }
                    
                }

            }
            else
            {
                print("Collides similar position");
            }
        }
        if (other.CompareTag("StopGame"))
        {
            myBallSoundManager.StopBallMoveSound();
            myGameManager_Script.GameOverWithAdv();
        }
        else
        {
            Debug.Log("The ball collides something unusual");   // sphere perhgaps :)
        }
    }

    void ChangeDictKey()
    {
        if (mySafePositions_Script != null)
        {
            mySafePositions_Script.ChangeKeyToDict(collideSafePosName);
            myRotateToBase_Script.TakeSafeRotAndPos();
        }
        else
        {
            Debug.Log("mSafePositions is null");
        }
    }

    IEnumerator MakeDust()
    {
        dust_PS.Play();
        yield return new WaitForSeconds(1 / 60f);
    }

}
