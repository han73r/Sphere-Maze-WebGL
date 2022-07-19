using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// control, keep and change best time, current time on each stage
/// https://www.youtube.com/watch?v=qc7J0iei3BU
/// </summary>

/*[DefaultExecutionOrder(-1000)]*/



/* early I've decided to use it as an Instance. I duno need this anymore. I've use DataManager for Instance instead */
public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [SerializeField] private TextMeshProUGUI currentTime_Text;
    [SerializeField] private TextMeshProUGUI currentSectorName_Text;

    [SerializeField] private TextMeshProUGUI bestScore_Text;
    [SerializeField] private TextMeshProUGUI bestScoreName_Text;

    [SerializeField] private TextMeshProUGUI sectorTime_Text;

    [SerializeField] private TextMeshProUGUI differenceTime_Text;

    [SerializeField] private GameObject bestScore_GO;
    [SerializeField] private GameObject currentTime_GO;
    [SerializeField] private GameObject sectorTime_GO;

    [SerializeField] private GameObject differenceTime_GO;
    [SerializeField] private GameObject blueBackground_GO;
    [SerializeField] private GameObject redBackground_GO;

    private float timeDeltaTime = -3f;                      // Total timer, -3 because game starts on 1.. 2.. 3.. (3sec delay)

    //private GameObject timerText_GO;

    TimeSpan timePlaying;

    [SerializeField] private bool isTimerGoing;

    [SerializeField] private float sectorCurrentTime;
    [SerializeField] private float sectorTotalTime;
    [SerializeField] private float totalTime;
    [SerializeField] private float differenceTime;

    [SerializeField] private float myBestSector1Time;
    [SerializeField] private float myBestSector2Time;
    [SerializeField] private float myBestSector3Time;
    [SerializeField] private float myBestTotalTime;

    private Color32 sector1_Color32;
    private Color32 sector2_Color32;
    private Color32 sector3_Color32;
    private Color32 total_Color32;

    [SerializeField] private int step = 0;                                  // delete after test

    private float showSectorTimeDelay = 2f;

    private string timePlaying_Str;
    private string sectorTotalTime_Str;

    private Coroutine coUpdateTimer;
    private Coroutine coShowBestResult;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        /*        DontDestroyOnLoad(gameObject);*/
    }

    void Start()
    {
        sector1_Color32 = new Color32(102, 145, 50, 178);
        sector2_Color32 = new Color32(240, 121, 0, 178);
        sector3_Color32 = new Color32(171, 58, 29, 178);
        total_Color32 = new Color32(24, 86, 118, 178);

        myBestSector1Time = GameData.Instance.BestSector1Time;    // min // should convert to ("mm':'ss'.'f") and back to float for save/load
        myBestSector2Time = GameData.Instance.BestSector2Time;
        myBestSector3Time = GameData.Instance.BestSector3Time;
        myBestTotalTime = GameData.Instance.BestTotalTime;

        isTimerGoing = false;

        currentTime_Text.text = "00:00.0";
        currentSectorName_Text.color = sector1_Color32;
        currentSectorName_Text.text = "Sector 1";
        sectorTime_Text.color = sector1_Color32;
    }

    void Update()
    {
        timeDeltaTime += Time.deltaTime;
/*
        if (Input.GetKey(KeyCode.P))
        {
            EndTimer();
            ShowResult();
            // launch IENUMERATOR THAT Pause Sector Time = SectorCurrent Time
            // 
        }
*/
        if (Input.GetKey(KeyCode.O))
        {
            BeginTimer();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            step = 1;
            ChangeColor_ShowBestSectorTime(step);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            step = 2;
            ChangeColor_ShowBestSectorTime(step);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            step = 3;
            ChangeColor_ShowBestSectorTime(step);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameData.Instance.DeleteScore();
        }
/*
                if (Input.GetKeyDown(KeyCode.J))
                {

                    StartCoroutine("ShowSectorTime");
                    // ShowNewSectorTime();
                }
*/
/*
        if (Input.GetKeyDown(KeyCode.M))
        {
            coShowBestResult = StartCoroutine(ShowSectorTime());
        }
*/
    }

    public void ChangeColor_ShowBestSectorTime(int step)
    {
        //ChangeTimerColor();
        coShowBestResult = StartCoroutine(ShowSectorTime(step));
        //StartCoroutine("ShowSectorTime");
    }

    // here change colors etc
    private IEnumerator ShowSectorTime(int step)
    {
        // here need to change sector color AFTER showing NEW current sector time

        // take Current time
        sectorTotalTime = sectorCurrentTime;
        sectorTotalTime_Str = timePlaying_Str;                                                          // may be sectorTime_Text.text = timePlaying_Str; ??
        sectorTime_Text.text = sectorTotalTime_Str;

        // restart Timer
        isTimerGoing = false;
        BeginTimer();

        // change sectorTime, current time color
        if (step == 1)
        {
            sectorTime_Text.color = sector1_Color32;
            bestScore_Text.text = TimeSpan.FromSeconds(myBestSector1Time).ToString("mm':'ss'.'f");

            // calculate the difference between best and current, save if new better then previos best
            differenceTime = sectorTotalTime - myBestSector1Time;
            differenceTime_Text.text = TimeSpan.FromSeconds(differenceTime).ToString("mm':'ss'.'f");

            if (differenceTime == sectorTotalTime || differenceTime < 0)                                // I added situation if there is no best time (1st play)
            {
                differenceTime_Text.text = ("- " + differenceTime_Text.text);
                myBestSector1Time = sectorTotalTime;                                                    // save new best sector score
                GameData.Instance.BestSector1Time = myBestSector1Time;
                GameData.Instance.SaveScore();
                if (differenceTime < 0)
                {
                    blueBackground_GO.SetActive(true);
                }
            }
            else if (differenceTime > 0)
            {
                differenceTime_Text.text = ("+ " + differenceTime_Text.text);
                redBackground_GO.SetActive(true);
            }

            currentTime_Text.color = sector2_Color32;
            currentSectorName_Text.color = sector1_Color32;
            currentSectorName_Text.text = "Sector 1";
        }
        else if (step == 2)
        {
            sectorTime_Text.color = sector2_Color32;
            bestScore_Text.text = TimeSpan.FromSeconds(myBestSector2Time).ToString("mm':'ss'.'f");

            // calculate the difference between best and current, save if new better then previos best
            differenceTime = sectorTotalTime - myBestSector2Time;
            differenceTime_Text.text = TimeSpan.FromSeconds(differenceTime).ToString("mm':'ss'.'f");
            if (differenceTime == sectorTotalTime || differenceTime < 0)
            {
                differenceTime_Text.text = ("- " + differenceTime_Text.text);
                myBestSector2Time = sectorTotalTime;                                                    // save new best sector score
                GameData.Instance.BestSector2Time = myBestSector2Time;
                GameData.Instance.SaveScore();
                if (differenceTime < 0)
                {
                    blueBackground_GO.SetActive(true);
                }
            }
            else if (differenceTime > 0)
            {
                differenceTime_Text.text = ("+ " + differenceTime_Text.text);
                redBackground_GO.SetActive(true);
            }

            currentTime_Text.color = sector3_Color32;
            currentSectorName_Text.color = sector2_Color32;
            currentSectorName_Text.text = "Sector 2";
        }
        else if (step == 3)
        {
            // last point! a bit difference, show Total Time at the end
            totalTime = timeDeltaTime;
            sectorTotalTime_Str = TimeSpan.FromSeconds(totalTime).ToString("mm':'ss'.'f");

            sectorTime_Text.color = sector3_Color32;
            bestScore_Text.text = TimeSpan.FromSeconds(myBestSector3Time).ToString("mm':'ss'.'f");

            // calculate the difference between best and current, save if new better then previos best
            differenceTime = sectorTotalTime - myBestSector3Time;
            differenceTime_Text.text = TimeSpan.FromSeconds(differenceTime).ToString("mm':'ss'.'f");
            if (differenceTime == sectorTotalTime || differenceTime < 0)
            {
                differenceTime_Text.text = ("- " + differenceTime_Text.text);
                myBestSector3Time = sectorTotalTime;                                                    // save new best sector score
                GameData.Instance.BestSector3Time = myBestSector3Time;
                GameData.Instance.SaveScore();
                if (differenceTime < 0)
                {
                    blueBackground_GO.SetActive(true);
                }
            }
            else if (differenceTime > 0)
            {
                differenceTime_Text.text = ("+ " + differenceTime_Text.text);
                redBackground_GO.SetActive(true);
            }

            //currentTimer_Text.color = total_Color32;
            currentSectorName_Text.color = sector3_Color32;
            currentSectorName_Text.text = "Sector 3";

            // stop main timer
            if (coUpdateTimer != null)
            {
                StopCoroutine(coUpdateTimer);
            }
        }

        // hide timer
        currentTime_GO.SetActive(false);

        // show saved current time
        sectorTime_GO.SetActive(true);

        // check 1st game with no sector result
        if (differenceTime != sectorTotalTime)
        {
            // show best result
            bestScore_GO.SetActive(true);

            // show difference time
            differenceTime_GO.SetActive(true);
        }

        yield return new WaitForSeconds(showSectorTimeDelay);         // something doesn't work here I stopped all coroutines at begin Timer)))))))) I use it to fixe timer restart..

        //print("Does Yield after 3 sec exists?");


        // hide saved current time
        sectorTime_GO.SetActive(false);

        // hide best result
        bestScore_GO.SetActive(false);

        // hide difference time
        differenceTime_GO.SetActive(false);
        blueBackground_GO.SetActive(false);
        redBackground_GO.SetActive(false);

        // change color of current sector name before show
        if (step == 1)
        {
            //currentTime_Text.color = sector1_Color32;
            currentSectorName_Text.color = sector2_Color32;
            currentSectorName_Text.text = "Sector 2";
        }
        else if (step == 2)
        {
            //sectorTime_Text.color = sector2_Color32;
            currentSectorName_Text.color = sector3_Color32;
            currentSectorName_Text.text = "Sector 3";
        }
        else if (step == 3)
        {
            //sectorTime_Text.color = sector3_Color32;
            currentTime_Text.color = total_Color32;
            currentTime_Text.text = sectorTotalTime_Str;

            bestScore_Text.text = TimeSpan.FromSeconds(myBestTotalTime).ToString("mm':'ss'.'f");

            currentSectorName_Text.color = total_Color32;
            currentSectorName_Text.text = "Total Time";



            differenceTime = totalTime - myBestTotalTime;
            differenceTime_Text.text = TimeSpan.FromSeconds(differenceTime).ToString("mm':'ss'.'f");
            if (differenceTime == totalTime || differenceTime < 0)
            {
                differenceTime_Text.text = ("- " + differenceTime_Text.text);
                myBestTotalTime = totalTime;                                                        // save new best sector score
                GameData.Instance.BestTotalTime = myBestTotalTime;
                GameData.Instance.SaveScore();
                if (differenceTime < 0)
                {
                    blueBackground_GO.SetActive(true);
                }
            }
            else if (differenceTime > 0)
            {
                differenceTime_Text.text = ("+ " + differenceTime_Text.text);
                redBackground_GO.SetActive(true);
            }

            if (differenceTime != totalTime)
            {
                bestScore_GO.SetActive(true);
                differenceTime_GO.SetActive(true);
            }


            ////////////////////////////////////////////////////////////////////////////////////////////
            // GAME STOP HERE // GAME STOP HERE // GAME STOP HERE // GAME STOP HERE // GAME STOP HERE //
            ////////////////////////////////////////////////////////////////////////////////////////////

            WinGame();


        }

        // show timer
        currentTime_GO.SetActive(true);

    }

    private void WinGame()
    {
        // innactive joy
        // compare best result in don't destroy on load here or early?
        // Show message like: Congradulations Go from start or menu
        // if bit previos score SHOW YOU BIT YOUR SCORE or NEW BEST SCORE IS:
    }

/*
    // delete this one
    void ChangeTimerColor()                                     // good, works as need
    {
        if (step == 1)
        {
            currentTime_Text.color = sector1_Color32;
            currentSectorName_Text.color = sector1_Color32;
            currentSectorName_Text.text = "Sector 1";

            sectorTime_Text.color = sector1_Color32;

            bestScore_Text.text = TimeSpan.FromSeconds(myBestSector1Time).ToString("mm':'ss'.'f"); // convert system

            step++;
        }
        else if (step == 2)
        {
            currentTime_Text.color = sector2_Color32;
            currentSectorName_Text.color = sector2_Color32;
            currentSectorName_Text.text = "Sector 2";

            sectorTime_Text.color = sector2_Color32;

            bestScore_Text.text = TimeSpan.FromSeconds(myBestSector2Time).ToString("mm':'ss'.'f");

            step++;
        }
        else if (step == 3)
        {
            currentTime_Text.color = sector3_Color32;
            currentSectorName_Text.color = sector3_Color32;
            currentSectorName_Text.text = "Sector 3";

            sectorTime_Text.color = sector3_Color32;

            bestScore_Text.text = TimeSpan.FromSeconds(myBestSector3Time).ToString("mm':'ss'.'f");

            step++;
        }
        else if (step == 4)
        {
            currentTime_Text.color = total_Color32;
            currentSectorName_Text.color = total_Color32;
            currentSectorName_Text.text = "Total Time";

            sectorTime_Text.color = total_Color32;

            bestScore_Text.text = TimeSpan.FromSeconds(myBestTotalTime).ToString("mm':'ss'.'f");

            step = 1;
        }

    }
*/
/*
    private void ShowResult()           // convert to Ienumrator: show for 3 sec and then new Sector Timer
    {
        bestScore_GO.SetActive(true);
    }
*/
    public void BeginTimer()
    {
        //StopAllCoroutines();                        // fix problem withUpdate
        //isTimerGoing = false;
        //StopCoroutine(UpdateTimer());             // don't fix problem withUpdate, what's wrong?
        //StopCoroutine(UpdateTimer());               // may be this fixed problem? No!

        isTimerGoing = false;
        if (coUpdateTimer != null)
        {
            StopCoroutine(coUpdateTimer);
        }


        sectorCurrentTime = 0f;
        //timePlaying = TimeSpan.FromSeconds(sectionCurrentTime); //No
        isTimerGoing = true;
        coUpdateTimer = StartCoroutine(UpdateTimer());
    }

    public void EndTimer()                 
    {
        isTimerGoing = false;
        StopCoroutine(UpdateTimer());
        //save current Time
        // show it for 3 sec
        // timer should go from start
    }



    private IEnumerator UpdateTimer()
    {
        while (isTimerGoing)
        {
            sectorCurrentTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(sectorCurrentTime);
            timePlaying_Str = timePlaying.ToString("mm':'ss'.'f");
            currentTime_Text.text = timePlaying_Str;

            yield return null;
        }
    }
}
