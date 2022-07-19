using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Add transition between scenes
/// </summary>

public class LoadingManager : MonoBehaviour
{
    public Animator transition;
    public GameObject loadingScreen;
    public static LoadingManager Instance;

    private string targetScene;
    private string currentScene;

    // choose different image from that Array
    public Sprite[] backgroundsTutorials;
    public Sprite[] backgroundsImages;
    public Image backgroundImage;

    [SerializeField] private GameObject loadingWheel;
    [SerializeField] private GameObject crossfade;

    [SerializeField] private float wheelSpeed;
    [SerializeField] private float minLoadTime;
    [SerializeField] private float transitionTime = 2f;
    private int myGameLaunchCount;

    private bool isLoading;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        loadingScreen.SetActive(false);                     // deactivate loading screen
        crossfade.gameObject.SetActive(false);              // deactivate fade
        Application.targetFrameRate = 60;                   // does it work? yes, but animation doesnt or something else wrong


        SceneManager.LoadSceneAsync((int)SceneData.MENU, LoadSceneMode.Additive);
    }

    private void Start()
    {
        GetLaunchCountNumber();
    }

    private void GetLaunchCountNumber()
    {
        myGameLaunchCount = GameData.Instance.gameLaunchCount;
    }

    public void LoadGame(string loadSceneName, string unloadSceneName)
    {
        targetScene = loadSceneName;
        print("targetScene is " + loadSceneName);
        currentScene = unloadSceneName;
        print("CurrentScene is " + currentScene);

        LaunchTransitionStart();

        // via fading
        //---StartCoroutine(TransitionRoutine());
        // StartCoroutine(LoadSceneRoutine());
    }

    private void LaunchTransitionStart()
    {
        crossfade.gameObject.SetActive(true);
        transition.SetTrigger("Start");
    }



    public void AscynLoadNextScene()
    {
        isLoading = true;

// >>> choose from array added here
        // Choose LoadingScreenImage
        ChooseLoadingScreenImg();
        
        // >>> backgroundImage.sprite = backgroundsImages[Random.Range(0, backgroundsImages.Length)];
        loadingScreen.SetActive(true);

        // loaging icon launch
        StartCoroutine(SpinWheelRoutine());
        StartCoroutine(LoadSceneRoutine());
    }

    private void ChooseLoadingScreenImg()
    {
        int lc = myGameLaunchCount;
        Image img = backgroundImage;
        Sprite[] imgT = backgroundsTutorials;
        Sprite[] imgR = backgroundsImages;

        if (lc == 0)
        {
            img.sprite = imgT[0];
        }
        else if (lc == 1)
        {
            img.sprite = imgT[1];
        }
        else if (lc == 2)
        {
            img.sprite = imgT[2];
        }
        else if (lc > 2 && lc < 9)
        {
            img.sprite = imgT[Random.Range(0, imgT.Length)];
        }
        else if (lc >= 9)
        {
            img.sprite = imgR[Random.Range(0, imgR.Length)];
        }
        // may add advertising after 20+ laucnhes or so
    }

    private IEnumerator LoadSceneRoutine()
    {
        
        SceneManager.UnloadSceneAsync(currentScene);
        AsyncOperation op = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
        float elapsedLoadTime = 0f;

        // wait Scene Loading
        while (!op.isDone)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }

        // min Loading Screen showing time
        while (elapsedLoadTime < minLoadTime)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }

        
        // loading ended. start transition (END)
        LaunchTransitionEnd();

    }

    private void LaunchTransitionEnd()
    {
        crossfade.gameObject.SetActive(true);
        transition.SetTrigger("End");
        
    }

    public void DisableLoadingScreen()
    {
        isLoading = false;
        loadingScreen.SetActive(false);

        // here find GameManager and CoundToStart
        GameObject myGameManagerGO;
        GameManager myGameManger;
        myGameManagerGO = GameObject.Find("GameManager");
        myGameManger = myGameManagerGO.GetComponent<GameManager>();
        myGameManger.CountToStart();
    }



    private IEnumerator SpinWheelRoutine()
    {
        while (isLoading)
        {
            loadingWheel.transform.Rotate(0, 0, -wheelSpeed);
            yield return null;
        }
    }

    public IEnumerator TransitionRoutine()
    {
        crossfade.gameObject.SetActive(true);
        //transition.SetTrigger("Start");
        //crossfade.gameObject.SetActive(false);
        //yield return new WaitForSeconds(2);
        transition.SetTrigger("Start");

        /// !!! 
        /// WORK IS HERE!
        /// !!!
        // add trigger instead WAIT!!!
        //        yield return new WaitForSeconds(2);
        ////crossfade.gameObject.SetActive(false);
        // turn off for now StartCoroutine(LoadSceneRoutine());
        yield return new WaitForSeconds(2);
        crossfade.gameObject.SetActive(false);
    }

    /* ---
        public void StartCoroutineLoadSceneRoutine()
        {
            // I gonna add disable/enable load screen here
            // then start coroutine all changes 
            // mark //--- that symbol
            //---
            bool isActive = loadingScreen.gameObject.activeSelf;
            if (isActive)
            {
                loadingScreen.SetActive(true);
            }
            else if (!isActive)
            {
                loadingScreen.SetActive(false);
            }
            //---
            StartCoroutine(LoadSceneRoutine());
        }
    ---*/

    /*---
        private IEnumerator LoadSceneRoutine()
        {
            isLoading = true;
            loadingScreen.SetActive(true);

            // loaging icon launch
            StartCoroutine(SpinWheelRoutine());

            SceneManager.UnloadSceneAsync(currentScene);
            AsyncOperation op = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
            float elapsedLoadTime = 0f;

            // wait Scene Loading
            while (!op.isDone)
            {
                elapsedLoadTime += Time.deltaTime;
                yield return null;
            }

            // show Loading Screen a bit
            while (elapsedLoadTime < minLoadTime)
            {
                elapsedLoadTime += Time.deltaTime;
                yield return null;
            }

            // show transition
            crossfade.gameObject.SetActive(true);
            transition.SetTrigger("End");
            yield return new WaitForSeconds(1);

            // disable loading screen
            loadingScreen.SetActive(false);
            yield return new WaitForSeconds(1);
            crossfade.gameObject.SetActive(false);

            isLoading = false;
        }
    ---*/





    /* shoda add coroutine with fade in/out */

    /* old loading ================================================================

        List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

        public void LoadGame()
        {
           StartCoroutine(TransitionToMain());
        }

        // transition from 0 to 100, then from 100 to 0
        public IEnumerator TransitionToMain()
        {
            // it's very weird but this animation works) // (Set atcive and it's fade out, then laucnh start from 100 to 0, dont ask why :)
            crossfade.gameObject.SetActive(true);

            yield return new WaitForSeconds(transitionTime);        
            loadingScreen.gameObject.SetActive(true);
            LoadMainScene();
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);
            crossfade.gameObject.SetActive(false);

        }

        private void LoadMainScene()
        {
            scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneData.MENU));
            scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneData.MAIN, LoadSceneMode.Additive)); 
            StartCoroutine(GetSceneLoadProgress());
        }

        public IEnumerator GetSceneLoadProgress()
        {
            for(int i=0; i<scenesLoading.Count; i++)
            {
                while (!scenesLoading[i].isDone)
                {
                    yield return null;
                }
            }

            yield return new WaitForSeconds(transitionTime);
            crossfade.gameObject.SetActive(true);  

            yield return new WaitForSeconds(transitionTime);
            loadingScreen.gameObject.SetActive(false);
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);
            crossfade.gameObject.SetActive(false);  
        }

    old loading end ================================================================ */


}
