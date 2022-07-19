using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fade event manager
/// </summary>
public class LoadLoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject myselfCrossfade;

    public void EventEnableLoadindScreen()
    {
        print("Load Loading Screen");
        LoadingManager.Instance.AscynLoadNextScene();
    }

    public void EventDisableLoadingScreen()
    {
        print("Unload Loading Screen");
        LoadingManager.Instance.DisableLoadingScreen();
    }

    public void DisableShade()
    {
        GameObject m = myselfCrossfade;
        m.gameObject.SetActive(false);
    }
}
