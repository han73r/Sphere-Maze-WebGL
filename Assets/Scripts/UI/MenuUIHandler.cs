using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

// All UI buttons settings are here
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] GameObject music;
    //[SerializeField]
    void Start()
    {

    }

    public void StartGame()
    {
        // add ball position
        // and later add Sphere from list or array
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void ReturnToMenu()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        //SceneManager.LoadScene(0);
        LoadingManager.Instance.LoadGame("Menu", "Main");
    }

    public void ReloadScene()
    {
        TimeManager.Instance.EndTimer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GameManager.
    }

    // changes may works at other scenes. 
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
}
