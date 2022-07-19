using UnityEngine;
public class CameraManager : MonoBehaviour
{
    public Camera mCamera;
    float startCameraSize = 1.93f;
    float gameCameraSize = 1.36f;
    
    /* public GameObject cam1;
     public GameObject cam2;
     void Update()
     {
         if (Input.GetButtonDown("ChooseCam1"))
         {
             cam1.SetActive(true);
             cam2.SetActive(false);
         }
         if (Input.GetButtonDown("ChooseCam2"))
         {
             cam1.SetActive(false);
             cam2.SetActive(true);
         }
     }*/
    public void MoveCamera()
    {
        mCamera.orthographicSize = Mathf.Lerp(startCameraSize, gameCameraSize, 0f);
    }
}