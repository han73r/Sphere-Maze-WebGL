using System.Collections;
using UnityEngine;
public class BinBehave : MonoBehaviour
{
    /* find all objects instead public (after MVP)*/
    public Transform pointTop;
    public GameObject sphere;                                                       // get the Angle of this obj to work with
    float /*quatTop, */quatBot, quatMid;                                                // pos to change view TOP
    float quatCheckSt, quatCheckFh/*, quatCheckObj*/;                                   // pos to change view LEFT and RIGHT
    Vector3 targetPos;
    public float sphereQuatX, /*sphereQuatY, sphereQuatZ, */sphereQuatW;            // sphere world Z angle value (0 to 360) // sphere world Z Quat value (-1 to 1)
    public float binQuatX, /*binQuatY, binQuatZ, */binQuatW;                        // bin world Z angle value (0 to 360)
    public float binXandW, multiXandW;                                                     // for Sign check
    //Vector3 correction;


    void Start()
    {
        targetPos = new Vector3(90, 0, 0); // rotate bin
        //correction = transform.localRotation.eulerAngles;
        //quatTop = 1;
        quatBot = 0;
        quatMid = 0.71f;
        quatCheckSt = 0.94f;
        quatCheckFh = 0.98f;
        //quatCheckObj = 0.17f;
    }
    void LookAtPoint()
    {
        Vector3 correction = new Vector3(0, 0, -90); // choose right one if will use somewhere else
        Vector3 direction = pointTop.position - (transform.position + correction);
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }
    void Update()
    {
        /* For test and define values */
        binQuatX = transform.rotation.x;
        //binQuatY = transform.rotation.y;
        //binQuatZ = transform.rotation.z;
        binQuatW = transform.rotation.w;

        sphereQuatX = sphere.transform.rotation.x;
        //sphereQuatY = sphere.transform.rotation.y;
        //sphereQuatZ = sphere.transform.rotation.z;
        sphereQuatW = sphere.transform.rotation.w;

        multiXandW = sphereQuatX * sphereQuatW;
        binXandW = binQuatX * binQuatW;

        /* Start Rotation */
        // if for top view 

        if (sphereQuatX > -quatMid && sphereQuatX < quatBot || sphereQuatX > quatBot && sphereQuatX < quatMid)
        {
            // We look UP to Point here
            // LookAtPoint();
        }
        else if (sphereQuatX < -quatCheckSt)
        {
            // multiXandW = sphereQuatX * sphereQuatW;
            if (Mathf.Sign(multiXandW) < 0)
            {
                // Sphere is on the left side, check bin right direction and rotate if need
                // check for RIGHT looking (may create method)
                // binXandW = binQuatX * binQuatW;
                if (Mathf.Sign(binXandW) > 0)
                {
                    // Bin looks right, rotate
                    StartCoroutine(LerpFunction(Quaternion.Euler(-targetPos), 0.5f));
                }
            }
            else
            {
                // Sphere is on the right side, check bin left direction and rotate if need
                // check for LEFT looking
                // binXandW = binQuatX * binQuatW;
                if (Mathf.Sign(binXandW) < 0)
                {
                    // Bin looks left, rotate
                    StartCoroutine(LerpFunction(Quaternion.Euler(targetPos), 0.5f));
                }
            }
        }
        else if (sphereQuatX > quatCheckFh)
        {
            // multiXandW = sphereQuatX * sphereQuatW;
            if (Mathf.Sign(multiXandW) > 0)
            {
                // Sphere is on the right side, check bin left direction and rotate if need
                // binXandW = binQuatW * binQuatW;
                if (Mathf.Sign(binXandW) < 0)
                {
                    // Bin looks left, rotate
                    StartCoroutine(LerpFunction(Quaternion.Euler(targetPos), 0.5f));
                }
            }
            else
            {
                // Sphere is on the left side, check bin right direction and rotate if need
                if (Mathf.Sign(binXandW) > 0)
                {
                    StartCoroutine(LerpFunction(Quaternion.Euler(-targetPos), 0.5f));
                }

            }
        }
    }

    IEnumerator LerpFunction(Quaternion endValue, float duration)
    {
        float time = 0;
        Quaternion startValue = transform.rotation;

        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endValue;
    }

}