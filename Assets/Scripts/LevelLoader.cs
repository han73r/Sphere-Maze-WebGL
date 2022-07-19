using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private GameObject crossfade;

    void Awake()
    {
        crossfade.SetActive(true);
    }

    // add new transition
    public void LoadManagerStart()
    {
        LoadingManager.Instance.LoadGame("Main", "Menu");
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Play animation
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(levelIndex);
    }
}
