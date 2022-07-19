using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject musicOnButton;
    public GameObject musicOffButton;

    public GameObject soundOnButton;
    public GameObject soundOffButton;

    private bool onMusic;
    private bool onSound;

    public GameObject musicSound;
    public GameObject soundBackground;


    void Start()
    {
        CheckMusicAndSoundStatus();
    }


    // use this to check music status from saved DATA
    void CheckMusicAndSoundStatus()                     // no check if GameData doesn't exists? I check it on GameData
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

    // use this to disable/enable sound
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

    // use this to disable/enable sound
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

}
