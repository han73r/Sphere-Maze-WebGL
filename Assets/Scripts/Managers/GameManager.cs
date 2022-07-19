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
    
    //public GameObject titleScreen;
    //public GameObject soundScreen;

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
    //public Button pauseButton;

    [SerializeField] private GameObject joy1;
    [SerializeField] private GameObject joy2;

    private Vector3 zeroJoyPos;

    private FloatingJoystick myFloatingJoystick1;
    private FloatingJoystick myFloatingJoystick2;

   /* [SerializeField] private GameObject joyHandle1;
    [SerializeField] private GameObject joyHandle2;

    [SerializeField] private GameObject joyBackground1;
    [SerializeField] private GameObject joyBackground2;*/
    
    [SerializeField] private float stopGameBallPosY;
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
    // private bool isPlaying;
    //public bool rotationOn;            // Restart rotation
    //float rotateSpeed = 25f;    // Rotation to start speed

    /*   void Awake() // may be awake before start?
       {

           mazeSpawnRot = Quaternion.Euler(90, 0, 0);
           baseRotPos = (mazePrefab.transform.rotation * mazeSpawnRot);
           // load prefabs
           SpawnMaze();
           SpawnBall();
           ball = GameObject.FindGameObjectWithTag("Ball");
           sphere = GameObject.FindGameObjectWithTag("Maze");
           mass = GameObject.FindGameObjectWithTag("Mass");
           ballRb = ball.GetComponent<Rigidbody>();
           baseRotPos = mass.transform.rotation.normalized;
           // change prefab position

       }*/

    private void Awake()
    {
        Time.timeScale = 1; // was a problem with sceneReloading, this is the fix
        //--- StartCoroutine(CountdownToStart()); // disable it here, because different launch speed
    }

    public void CountToStart()
    {
        StartCoroutine(CountdownToStart());
    }

    /*
    void StartTimer()
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        TimeManager.Instance.BeginTimer();
    }
    */

    private void Start()
    {
        CheckMusicAndSoundStatus();
        zeroJoyPos = new Vector3(0f, 0f, 0f);
        // EnableOrDisableMusic();
        // EnableOrDisableSound();


        // ------ input system
        //PlayerInput input = GetComponent<PlayerInput>();
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

        // need to add ball pos from button
        //titleScreen.gameObject.SetActive(false);
        //soundScreen.gameObject.SetActive(false);

        //fing Pause Button GameoBject
        pauseButton = gameHUD.transform.Find("Pause Button").gameObject;

        //find rotate to base script
        myRotateToBase = myMassCenter.GetComponent<RotateToBase>();
        myJoystickRotate = myMassCenter.GetComponent<JoystickRotate>();

        myFloatingJoystick1 = joy1.GetComponent<FloatingJoystick>();
        myFloatingJoystick2 = joy2.GetComponent<FloatingJoystick>();

        /*StartCoroutine(CountdownToStart());*/

        /*
        mazeSpawnRot = Quaternion.Euler(90, 0, 0);
        baseRotPos = (mazePrefab.transform.rotation * mazeSpawnRot);
       */

        // load prefabs
        /*SpawnMaze();*/
        SpawnBall();

        ball = GameObject.FindGameObjectWithTag("Ball");
        sphere = GameObject.FindGameObjectWithTag("Maze");
        mass = GameObject.FindGameObjectWithTag("Mass");

        ballRb = ball.GetComponent<Rigidbody>();
        // ballAudioSource = ball.GetComponent<AudioSource>(); ball haven't audio source

        stopGameBallPosY = -1000.468f;  // do I still need it???!

        baseRotPos = mass.transform.rotation.normalized;
        /*miniCam.gameObject.SetActive(true);*/
        inGameScreen.gameObject.SetActive(true);

        Transform sphereTr = sphere.GetComponent<Transform>();
        ball.transform.SetParent(sphereTr);

        EnableOrDisableGravity();

        EnableInput();

        Time.timeScale = 1;
        onPause = false;
        //isPlaying = true;

        TimeManager.Instance.BeginTimer();

        /*StartTimer();*/

        // Start Timer


    }
    private void EnableInput()
    {
        joy1.gameObject.SetActive(true);
        joy2.gameObject.SetActive(true);
        myFloatingJoystick1.RestartJoystickInit();
        myFloatingJoystick2.RestartJoystickInit();
        
        //myJoystickRotate.EnableJoyRotate();
        //print("Input enabled");
    }

    private void DisableInput()
    {
        
        //joy1.gameObject.transform.position = zeroJoyPos;
        //joy2.gameObject.transform.position = zeroJoyPos;

        //joyBackground1.gameObject.transform.position = zeroJoyPos;
       // joyBackground2.gameObject.transform.position = zeroJoyPos;

        // this should disable bug with pushed joy 
        joy1.gameObject.SetActive(false);
        joy2.gameObject.SetActive(false);
        
        
        //myJoystickRotate.DisableJoyRotate();
       // joyHandle1.gameObject.transform.localPosition = zeroJoyPos;
        //joyHandle2.gameObject.transform.localPosition = zeroJoyPos;
        //myJoystickRotate.DisableJoyRotate();
        //print("Input disabled");
    }

    private void Update()
    {
        // check scene restart
        // and start game
        /*  if (Input.GetKey(KeyCode.R) || ball.transform.position.y < -100)
          {
              RestartLevel();
          }*/
        if (Input.GetKeyDown(KeyCode.G))
        {
            EnableOrDisableGravity();
        }

        if (ball != null && ball.transform.position.y < stopGameBallPosY)
        {
            GameOverWithAdv();
        }

        // add to start game count
        if (Input.GetKeyDown(KeyCode.L))
        {
            CountToStart();
            print("Button L used to start game");
        }

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
        //BallCount();

        //ballRb.detectCollisions = false;                // add true afterall // doesn't work
        
        //DisableInput();

        HealthControl.Health -= 1;
        onPause = true;
        // add disable/enable Joy
        
        Time.timeScale = 0;
        ballRb.detectCollisions = false;                    // The ball don't detect safe positions when moving to safe cooridnate
        // check ball Rb between 15 and 18 stages (safe postitins are not RB, it scripted boxes!)

        



//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // ballRb.gameObject.SetActive(false); - shoud find child AudioSocrse object and stop looped sound
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

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
                              // works?
        // should add here Health change script
        /*if (HealthControl.Health == 1)
        {
            totalGameOverScreen.gameObject.SetActive(true);
            return;
        }*/
        BallToSpawn();                                    // return ball to start pos or GameOver screen lasts forever because ball under coordiate
        LaunchAdv();
        inGameScreen.gameObject.SetActive(true);
        


        onPause = false;
        Time.timeScale = 1;
        myRotateToBase.rotatingOn = true;
        ballRb.detectCollisions = true;
        
        //EnableInput();

    }

    private void LaunchAdv()
    {
        print("Launch advertising");
    }

/*
    private void DirectPause()
    {
        onPause = true;
        Time.timeScale = 0;
    }
*/
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
            /*miniCam.gameObject.SetActive(false);*/
            pauseScreen.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);

            //ballRb.gameObject.SetActive(false);

            // soundScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
            DisableInput();
            onPause = true;
        }
        else
        {
            inGameScreen.gameObject.SetActive(true);
            /*miniCam.gameObject.SetActive(true);*/
            pauseScreen.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);

            //ballRb.gameObject.SetActive(true);

            //soundScreen.gameObject.SetActive(false);
            Time.timeScale = 1;
            EnableInput();
            onPause = false;
        }
    }

    private void SpawnMaze()
    {
        //mazeSpawnRot = Quaternion.Euler(90, 0, 0);
        // make Maze as Child of Mass Cneter
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
        //rotationOn = true;
        //RotateToBase();
        // rotateScript;
        SpawnBall();
    }

    public void WinGame()
    {
        winTitleScreen.gameObject.SetActive(true);
        winScreen.gameObject.SetActive(true);
    }
    /*
        public void EnableAndDisableMusic()
        {
            if (music.gameObject.activeSelf)
            {
                music.gameObject.SetActive(false);
            }
            else
            {
                music.gameObject.SetActive(true);
            }
        }
    */
    /*
        void RotateToBase()
        {
            if (rotationOn)
            {
                var step = rotateSpeed * Time.deltaTime;
                Quaternion st = transform.rotation.normalized;
                Quaternion fn = baseRotPos;
                mass.transform.rotation = Quaternion.RotateTowards(st, baseRotPos, step);
            }
            / (mass.transform.rotation == target.rotation)
            {
                Debug.Log("rotation false");
                rotationOn = false;
            }
            // return Maze to base Rotate
        }
    */
    private void BallCount()
    {
        // -1 ball count
    }
    /*
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void GameQuit()
        {
            Application.Quit();
            // add rate here
        }
    */
}
