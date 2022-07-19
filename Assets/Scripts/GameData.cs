using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// DDOL class for values between scenes
/// Such as best time, sound off/on and 2 and 3rd safe positions to start from 2nd and 3rd position
/// <summary>

public class GameData : MonoBehaviour
{

    // shoud I use Encapsulation here?
    public static GameData Instance;

    [SerializeField] private float bestSector1Time;
    [SerializeField] private float bestSector2Time;
    [SerializeField] private float bestSector3Time;
    [SerializeField] private float bestTotalTime;

    //private bool onMusic {set;get;}
    //private bool onSound {set;get;}
    // change to upper when you'll ready)

    [SerializeField] private int _gameLaunchCount;
    public int gameLaunchCount
    {
        get {return _gameLaunchCount;}
    }

    //    [SerializeField] private int nextGameLaunchCount;

    public bool onMusic;
    public bool onSound;


    /* add inf about launch time (for turotials and advertising) here
        public void LoadGameLaunchData()
        {

        }
        public void SaveGameLaunchData()
        {

        }

        [System.Serializable]
        public class LaunchData
        {
            public bool launchedTimes;
        }
    */

    public float BestSector1Time
    {
        get { return bestSector1Time; }
        set
        {
            if (value < bestSector1Time || bestSector1Time == 0f)
                bestSector1Time = value;
        }
    }
    public float BestSector2Time
    {
        get { return bestSector2Time; }
        set
        {
            if (value < bestSector2Time || bestSector2Time == 0f)
                bestSector2Time = value;
        }
    }
    public float BestSector3Time
    {
        get { return bestSector3Time; }
        set
        {
            if (value < bestSector3Time || bestSector3Time == 0f)
                bestSector3Time = value;
        }
    }
    public float BestTotalTime
    {
        get { return bestTotalTime; }
        set
        {
            if (value < bestTotalTime || bestTotalTime == 0f)
                bestTotalTime = value;
        }
    }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();

        LoadSoundData();

        LoadCountData();
        /*
                bestSector1Time = 0f;
                bestSector2Time = 0f;   // 5.856f;
                bestSector3Time = 0f;   // 6.675f;
                bestTotalTime = 0f;     // 13.567f;
        */
    }

    [System.Serializable]
    public class CountData
    {
        public int curData;         // currentGameLaunchCount;
    }

    private void LoadCountData()
    {
        // if doesn't exist - create, and Console delete script
        // loaded
        int curData = _gameLaunchCount;

        string path = Application.persistentDataPath + "/countfile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            CountData data = JsonUtility.FromJson<CountData>(json);

            curData = data.curData;
            _gameLaunchCount = curData;
            SaveCountData();
        }
        else
        {
            _gameLaunchCount = 0;
            SaveCountData();
        }

        print("Count data Loaded");
        print("LaunchCount number is " + curData);
    }

    private void SaveCountData()
    {
        //save
        //beforeSave +1 for next launch
        //gameLaunchCount++;
        int curData = _gameLaunchCount;
        //        int nextData = nextGameLaunchCount;
        curData++;

        CountData data = new CountData();

        data.curData = curData;
        //        data.nextData = nextData;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/countfile.json", json);

        print("LaunchCount data Saved");
    }

    private void DeleteLaunchData()
    {
        string path = Application.persistentDataPath + "/countfile.json";
        if (File.Exists(path))
        {
            File.Delete(Application.persistentDataPath + "/countfile.json");
        }
    }

    [System.Serializable]
    public class SoundData
    {
        public bool savedOnMusic;
        public bool savedOnSound;
    }

    public void EnableAndDisableMusic()
    {
        if (!onMusic)               // Music was switched off early. Now Enable
        {
            onMusic = true;
        }
        else if (onMusic)           // Music was switched on early. Now Disable
        {
            onMusic = false;
        }
        SaveSoundData();
    }

    public void EnableOrDisableSound()
    {
        if (!onSound)               // Sound was switched off early. Now Enable
        {
            onSound = true;
        }
        else if (onSound)           // Sound was switched on early. Now Disable
        {
            onSound = false;
        }

        SaveSoundData();
    }


    private void SaveSoundData()
    {
        SoundData data = new SoundData();

        data.savedOnMusic = onMusic;
        data.savedOnSound = onSound;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/soundfile.json", json);

        print("Sound Data Saved");
    }

    /* use this one when game starts */
    private void LoadSoundData()
    {
        string path = Application.persistentDataPath + "/soundfile.json";
        if (File.Exists(path))
        {
            //take data
            string json = File.ReadAllText(path);
            SoundData data = JsonUtility.FromJson<SoundData>(json);

            onMusic = data.savedOnMusic;
            onSound = data.savedOnSound;

            print("Sound data succesfully loaded");
        }
        else
        {
            onMusic = true;
            onSound = true;

            print("sound data file doesn't exist so anything set as true");
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            print("delete button = Delete Score() and LauchData()");
            DeleteScore();
            print("The Best Score file was deleted");
            DeleteLaunchData();
            print("Launch count file was deleted");
        }

    }

    [System.Serializable]
    public class ScoreData
    {
        public float savedBestSector1Time;
        public float savedBestSector2Time;
        public float savedBestSector3Time;
        public float savedBestTotalTime;
    }

    public void SaveScore()
    {
        ScoreData data = new ScoreData();

        data.savedBestSector1Time = bestSector1Time;
        data.savedBestSector2Time = bestSector2Time;
        data.savedBestSector3Time = bestSector3Time;
        data.savedBestTotalTime = bestTotalTime;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        print("Score Saved");
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);

            bestSector1Time = data.savedBestSector1Time;
            bestSector2Time = data.savedBestSector2Time;
            bestSector3Time = data.savedBestSector3Time;
            bestTotalTime = data.savedBestTotalTime;

            print("Score Loaded");
        }
        else
        {
            bestSector1Time = 0f;
            bestSector2Time = 0f;
            bestSector3Time = 0f;
            bestTotalTime = 0f;

            print("Best Score file doesn't exist");
        }
    }

    public void DeleteScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            File.Delete(Application.persistentDataPath + "/savefile.json");
        }
        bestSector1Time = 0f;
        bestSector2Time = 0f;
        bestSector3Time = 0f;
        bestTotalTime = 0f;
    }

}