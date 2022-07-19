using System.Collections;
using UnityEngine;

/// <summary>
/// AShow and hide UI
/// </summary>

public class ShowPhoneEXP : MonoBehaviour
{
    [SerializeField] private Animator transitionUI;
    private int waitTime;

    private void Start()
    {
        waitTime = 3;
        transitionUI.enabled = true;
        StartCoroutine(ShowUI());
    }

    private IEnumerator ShowUI()
    {
        print("Launch UI animator");
        transitionUI.SetTrigger("Start");
        yield return new WaitForSeconds(waitTime);
        transitionUI.SetTrigger("End");
        yield return new WaitForSeconds(waitTime);
        transitionUI.enabled = false;
    }

}
