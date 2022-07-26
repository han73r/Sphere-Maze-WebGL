using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


// Sphere loading from prefab is here, gameCases and other
public class GameManager : MonoBehaviour
{
    public GameObject mazePrefab;
    public GameObject ballPrefab;
    public GameObject myMassCenter;

    public GameObject musicSound;           // use to disable music
    public GameObject soundSound;           // use to disable sound // I don't understand. what object is this?

    public GameObject musicOnButton;
    public GameObject musicOffButton;

    public GameObject soundOnButton;
    public GameObject soundOffButton;

    public GameObject soundBackground;
    
    public GameObject inGameScreen;
    public GameObject gameHUD;
    
    public GameObject pauseScreen;

    public GameObject advertiseScreen;
    public GameObject gameOverTitleScreen;
    public GameObject totalGameOverScreen;
    
    public GameObject winTitleScreen;
    public GameObject winScreen;

    public Transform target;
    public Button startButton;

    [SerializeField] private GameObject joy1;
    [SerializeField] private GameObject joy2;

    private Vector3 zeroJoyPos;

    private FloatingJoystick myFloatingJoystick1;
    private FloatingJoystick myFloatingJoystick2;

    [SerializeField] private int countdownTime;
    [SerializeField] private Text countdownDisplay;
    [SerializeField] private bool onPause = false;

    private GameObject pauseButton;
    private GameObject sphere;
    private GameObject ball;
    private GameObject mass;

    private TimeManager myTimeManager; // never used?
    private RotateToBase myRotateToBase;
    private JoystickRotate myJoystickRotate;
    
    private Button startBtn;

    private bool onMusic;            // save it and use fro DDOL
    private bool onSound;

    private Vector3 ballSpawnPos;
    private Vector3 mazeSpawnPos;

    private Quaternion mazeSpawnRot;
    private Quaternion baseRotPos;
    
    private Rigidbody ballRb;
    private AudioSource ballAudioSource;

    private void Awake()
    {
        Time.timeScale = 1; // was a problem with sceneReloading, this is the fix
    }

    private void Start()
    {
        CheckMusicAndSoundStatus();
        zeroJoyPos = new Vector3(0f, 0f, 0f);
        StartCoroutine(CountdownToStart());
    }

    private void CheckMusicAndSoundStatus()                     // no check if GameData doesn't exists? I check it on GameData
    {
        onMusic = GameData.Instance.onMusic;
        onSound = GameData.Instance.onSound;
        if (onMusic == true)
        {
            musicSound.gameObject.SetActive(true);

            musicOnButton.gameObject.SetActive(true);
            musicOffButton.gameObject.SetActive(false);
        }
        else
        {
            musicSound.gameObject.SetActive(false);

            musicOnButton.gameObject.SetActive(false);
            musicOffButton.gameObject.SetActive(true);
        }
        if (onSound == true)
        {
            soundBackground.gameObject.SetActive(true);

            soundOnButton.gameObject.SetActive(true);
            soundOffButton.gameObject.SetActive(false);
        }
        else
        {
            soundBackground.gameObject.SetActive(false);

            soundOnButton.gameObject.SetActive(false);
            soundOffButton.gameObject.SetActive(true);
        }
    }

    /* I can add save for game data here too */
    public void EnableOrDisableMusic()
    {
        if (!GameData.Instance.onMusic)                     // Music was switched off early. Now Enable
        {
            musicSound.gameObject.SetActive(true);
            musicOnButton.gameObject.SetActive(true);
            musicOffButton.gameObject.SetActive(false);
            GameData.Instance.EnableAndDisableMusic();
        }
        else if (GameData.Instance.onMusic)                 // Music was switched on early. Now Disable
        {
            musicSound.gameObject.SetActive(false);
            musicOnButton.gameObject.SetActive(false);
            musicOffButton.gameObject.SetActive(true);
            GameData.Instance.EnableAndDisableMusic();
        }
    }

    public void EnableOrDisableSound()
    {
        if (!GameData.Instance.onSound)                     // Sound was switched off early. Now Enable
        {
            // enable all background sounds and etc
            soundBackground.gameObject.SetActive(true);
            

            soundOnButton.gameObject.SetActive(true);
            soundOffButton.gameObject.SetActive(false);
            GameData.Instance.EnableOrDisableSound();
        }
        else if (GameData.Instance.onSound)                 // Sound was switched on early. Now Disable
        {
            // disable all background sounds and etc
            soundBackground.gameObject.SetActive(false);

            soundOnButton.gameObject.SetActive(false);
            soundOffButton.gameObject.SetActive(true);
            GameData.Instance.EnableOrDisableSound();
        }
    }

    public void StartGame()
    {
        //fing Pause Button GameoBject
        pauseButton = gameHUD.transform.Find("Pause Button").gameObject;

        //find rotate to base script
        myRotateToBase = myMassCenter.GetComponent<RotateToBase>();
        myJoystickRotate = myMassCenter.GetComponent<JoystickRotate>();

        myFloatingJoystick1 = joy1.GetComponent<FloatingJoystick>();
        myFloatingJoystick2 = joy2.GetComponent<FloatingJoystick>();

        SpawnBall();

        ball = GameObject.FindGameObjectWithTag("Ball");
        sphere = GameObject.FindGameObjectWithTag("Maze");
        mass = GameObject.FindGameObjectWithTag("Mass");

        ballRb = ball.GetComponent<Rigidbody>();

        baseRotPos = mass.transform.rotation.normalized;
        inGameScreen.gameObject.SetActive(true);

        Transform sphereTr = sphere.GetComponent<Transform>();
        ball.transform.SetParent(sphereTr);

        EnableOrDisableGravity();

        EnableInput();

        Time.timeScale = 1;
        onPause = false;

        TimeManager.Instance.BeginTimer();
    }

    private void EnableInput()
    {
        joy1.gameObject.SetActive(true);
        joy2.gameObject.SetActive(true);
        myFloatingJoystick1.RestartJoystickInit();
        myFloatingJoystick2.RestartJoystickInit();
    }

    private void DisableInput()
    {
        joy1.gameObject.SetActive(false);
        joy2.gameObject.SetActive(false);
    }

    private IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";

        StartGame();
        pauseButton.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }


    public void GameOverWithAdv()
    {
        HealthControl.Health -= 1;
        onPause = true;
        // add disable/enable Joy
        
        Time.timeScale = 0;
        ballRb.detectCollisions = false;                    // The ball don't detect safe positions when moving to safe cooridnate
        // check ball Rb between 15 and 18 stages (safe postitins are not RB, it scripted boxes!)

        if (HealthControl.Health == 0)
        {
            gameOverTitleScreen.gameObject.SetActive(true);
            totalGameOverScreen.gameObject.SetActive(true);
            return;
        }
        gameOverTitleScreen.gameObject.SetActive(true);
        advertiseScreen.gameObject.SetActive(true);
        inGameScreen.gameObject.SetActive(false);
    }

    public void WatchAdv()
    {
        advertiseScreen.gameObject.SetActive(false);
        gameOverTitleScreen.gameObject.SetActive(false);
        BallToSpawn();                                    // return ball to start pos or GameOver screen lasts forever because ball under coordiate
        LaunchAdv();
        inGameScreen.gameObject.SetActive(true);
        


        onPause = false;
        Time.timeScale = 1;
        myRotateToBase.rotatingOn = true;
        ballRb.detectCollisions = true;
    }

    private void LaunchAdv()
    {
        print("Launch advertising");
    }

    private void GameOver()
    {
        BallCount();
        TimeManager.Instance.EndTimer();
        inGameScreen.gameObject.SetActive(false);
        advertiseScreen.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        if (!onPause)
        {
            inGameScreen.gameObject.SetActive(false);
            pauseScreen.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
            Time.timeScale = 0;
            DisableInput();
            onPause = true;
        }
        else
        {
            inGameScreen.gameObject.SetActive(true);
            pauseScreen.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);
            Time.timeScale = 1;
            EnableInput();
            onPause = false;
        }
    }

    private void SpawnMaze()
    {
        Vector3 move = new Vector3(.017f, 0, 1.24f);
        GameObject go = Instantiate(mazePrefab, mazeSpawnPos + move, baseRotPos) as GameObject;
        go.transform.parent = GameObject.Find("Mass Center").transform;

    }
    private void SpawnBall()
    {
        // check another balls
        ballSpawnPos = new Vector3(.144f, .325f, 1f);

        // Spawn Inactive ball. Gravity Doesntwork
        if (ball != null)
        {
            Destroy(ball);
            return;
        }
        if (!ball)
        {
            Instantiate(ballPrefab, ballSpawnPos, ballPrefab.transform.rotation);
        }
        else
        {
            ballRb.velocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;
            ball.transform.position = ballSpawnPos;
            ball.transform.rotation = ballPrefab.transform.rotation;
        }
    }

    private void BallStopMoving()
    {
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
        ball.transform.position = ballSpawnPos;
    }

    private void BallToSpawn()
    {
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
        ball.transform.position = ballSpawnPos;
        ball.transform.rotation = ballPrefab.transform.rotation;
    }

    private void EnableOrDisableGravity()
    {
        if (!ballRb.useGravity)
        {
            ballRb.useGravity = true;
            Debug.Log("Gravity: On");
        }
        else
        {
            ballRb.useGravity = false;
            Debug.Log("Gravity: Off");
        }
    }
    private void DisableGravity()
    {
        if (ball)
        {
            ballRb.useGravity = false;
        }
    }
    // Update is called once per frame
    private void RestartLevel()
    {
        DisableGravity();
        BallCount();
        SpawnBall();
    }

    public void WinGame()
    {
        winTitleScreen.gameObject.SetActive(true);
        winScreen.gameObject.SetActive(true);
    }
    
    private void BallCount()
    {
        // -1 ball count
    }
}
