using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/* // DEL // DEL // DEL // DEL // DEL // DEL // DEL // DEL // DEL // DEL // DEL // DEL // DEL
Moved to GameManager!
*/ // DEL // DEL // DEL // DEL // DEL // DEL // DEL // DEL // DEL // DEL // DEL // DEL // DEL

public class CountdownController : MonoBehaviour
{
    [SerializeField] private int countdownTime;
    [SerializeField] private Text countdownDisplay;

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        /*GameManager.Instantiate.StartGame();*/

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }

}
