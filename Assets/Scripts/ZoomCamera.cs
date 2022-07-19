using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    
    public GameObject startButton;

    [SerializeField] private bool isZoomed;
    private bool isLoading;
    private Camera mCamera;
    private float startCameraSize = 1.93f;
    private float gameCameraSize = 1.36f;
    private float zoomSpeed = 0.007f;
    [SerializeField] private float camOrtoSize;

    void Start()
    {
        mCamera = Camera.main;
    }

    // !!!!
    // should create coroutine here instead!
    void LateUpdate()
    {
        camOrtoSize = mCamera.orthographicSize;

        if (isZoomed)
        {
            mCamera.orthographicSize = Mathf.Lerp(mCamera.orthographicSize,gameCameraSize,zoomSpeed);
        }
        /*else
        {
           // mCamera.orthographicSize = Mathf.Lerp(mCamera.orthographicSize,startCameraSize,zoomSpeed); // use if you need it
        }*/

// launch this several times
        if(isLoading && isZoomed && camOrtoSize < (gameCameraSize + 0.4f))
        {
            LoadingManager.Instance.LoadGame("Main", "Menu");
            isLoading = false;
            
            if (camOrtoSize < (gameCameraSize + 0.3f))
            {
                isZoomed = false;
            }
        }

    }

    public void StartButtonEnabled()
    {
        isLoading = true;
        isZoomed = true;
        startButton.gameObject.SetActive(false);        
        // wait a bit
        
       //LoadingManager.Instance.LoadGame("Main", "Menu");
    }

}
