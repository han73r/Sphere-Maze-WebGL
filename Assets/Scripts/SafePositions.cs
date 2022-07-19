using System.Collections.Generic;
using UnityEngine;

public class SafePositions : MonoBehaviour
{
    // Dictionaries for all safe sphere Rotate and ball Position
    Dictionary<string, Vector3> sphRotDict = new Dictionary<string, Vector3>();
    Dictionary<string, Vector3> ballPosDict = new Dictionary<string, Vector3>();

    // all safe positions
    // start position
    Vector3 sphereRot0 = new Vector3(0f, 0f, 0f);
    Vector3 ballPos0 = new Vector3(0.144f, 0.325f, 1f);
    // 1st
    Vector3 sphereRot1 = new Vector3(-0.275f, 161.162f, 57.582f);
    Vector3 ballPos1 = new Vector3(0.726f, .0f, -0.062f);
    // 2nd
    Vector3 sphereRot2 = new Vector3(0f, 180f, -1.5f);
    Vector3 ballPos2 = new Vector3(0f, 0.311f, -1f);
    // 3rd
    Vector3 sphereRot3 = new Vector3(-5.6f, 161f, -96f);
    Vector3 ballPos3 = new Vector3(0.046f, 0.815f, -0.159f);
    // 4th
    Vector3 sphereRot4 = new Vector3(0f, -10f, 90f);
    Vector3 ballPos4 = new Vector3(0.347f, -0.656f, 0.185f);
    // 5th
    Vector3 sphereRot5 = new Vector3(-100f, 110f, -20f);
    Vector3 ballPos5 = new Vector3(0.516f, 0.204f, -0.687f);
    //6th
    Vector3 sphereRot6 = new Vector3(-3f, -56f, -124f);
    Vector3 ballPos6 = new Vector3(0.6f, 0.06f, 0.4f);
    //7th
    Vector3 sphereRot7 = new Vector3(-35f, -40f, 83f);
    Vector3 ballPos7 = new Vector3(0.368f, 0.157f, -0.236f);
    //8th
    Vector3 sphereRot8 = new Vector3(9f, 47f, 173f);
    Vector3 ballPos8 = new Vector3(0.53f, 0.253f, 0.254f);
    //9th REAL THIRD POINT
    Vector3 sphereRot9 = new Vector3(0f, 90f, -5f);
    Vector3 ballPos9 = new Vector3(1f, 0.3f, 0.17f);
    //10th
    Vector3 sphereRot10 = new Vector3(-87f, -55f, 112f);
    Vector3 ballPos10 = new Vector3(0.0052f, 0.102f, 0.005f);
    //11th
    Vector3 sphereRot11 = new Vector3(171f, 14f, 2f);
    Vector3 ballPos11 = new Vector3(0.85f, 0.04f, -0.14f);
    //12th
    Vector3 sphereRot12 = new Vector3(17f, 45f, 6f);
    Vector3 ballPos12 = new Vector3(0.6f, 0.08f, -0.21f);
    //13th
    Vector3 sphereRot13 = new Vector3(179f, 18f, -53f);
    Vector3 ballPos13 = new Vector3(0.72f, 0.055f, 0.41f);
    //14th
    Vector3 sphereRot14 = new Vector3(-2.5f, -93f, 1f);
    Vector3 ballPos14 = new Vector3(0.82f, 0.032f, -0.28f);
    //15th
    Vector3 sphereRot15 = new Vector3(177f, 92f, 0f);
    Vector3 ballPos15 = new Vector3(0.5f, 0.17f, -0.12f);
    //16th
    Vector3 sphereRot16 = new Vector3(38f, -111f, -73f);
    Vector3 ballPos16 = new Vector3(-0.17f, -0.27f, -0.89f);
    //17th
    Vector3 sphereRot17 = new Vector3(173f, 8f, -1f);
    Vector3 ballPos17 = new Vector3(0.3f, 0.2f, -0.67f);
    //18th
    Vector3 sphereRot18 = new Vector3(-1.6f, 16f, 2f);
    Vector3 ballPos18 = new Vector3(0.23f, 0.11f, 0.36f);

    int dictLength;                         // Dictionaries Length
    int posCount = 0;                       // value to change safepose
    string keytoDict;                       // field
    public string KeyToDict                 // property key to dicionaries
    {
        get
        {
            return keytoDict;
        }
        set
        {
            keytoDict = value;
        }
    }
    public string newKeyToDict;             // value from another classes
    Vector3 safeSphereRotation { get; }
    public Vector3 SafeSphereRotation { get; set; }
    Vector3 safeBallPosition { get; }
    public Vector3 SafeBallPosition { get; set; }

    void Awake()
    {
        // add all Sphere safe rotations to dictionary
        sphRotDict.Add("pos0", sphereRot0);
        sphRotDict.Add("pos1", sphereRot1);
        sphRotDict.Add("pos2", sphereRot2);
        sphRotDict.Add("pos3", sphereRot3);
        sphRotDict.Add("pos4", sphereRot4);
        sphRotDict.Add("pos5", sphereRot5);
        sphRotDict.Add("pos6", sphereRot6);
        sphRotDict.Add("pos7", sphereRot7);
        sphRotDict.Add("pos8", sphereRot8);
        sphRotDict.Add("pos9", sphereRot9);
        sphRotDict.Add("pos10", sphereRot10);
        sphRotDict.Add("pos11", sphereRot11);
        sphRotDict.Add("pos12", sphereRot12);
        sphRotDict.Add("pos13", sphereRot13);
        sphRotDict.Add("pos14", sphereRot14);
        sphRotDict.Add("pos15", sphereRot15);
        sphRotDict.Add("pos16", sphereRot16);
        sphRotDict.Add("pos17", sphereRot17);
        sphRotDict.Add("pos18", sphereRot18);

        // add all Ball safe positions to dictionary
        ballPosDict.Add("pos0", ballPos0);
        ballPosDict.Add("pos1", ballPos1);
        ballPosDict.Add("pos2", ballPos2);
        ballPosDict.Add("pos3", ballPos3);
        ballPosDict.Add("pos4", ballPos4);
        ballPosDict.Add("pos5", ballPos5);
        ballPosDict.Add("pos6", ballPos6);
        ballPosDict.Add("pos7", ballPos7);
        ballPosDict.Add("pos8", ballPos8);
        ballPosDict.Add("pos9", ballPos9);
        ballPosDict.Add("pos10", ballPos10);
        ballPosDict.Add("pos11", ballPos11);
        ballPosDict.Add("pos12", ballPos12);
        ballPosDict.Add("pos13", ballPos13);
        ballPosDict.Add("pos14", ballPos14);
        ballPosDict.Add("pos15", ballPos15);
        ballPosDict.Add("pos16", ballPos16);
        ballPosDict.Add("pos17", ballPos17);
        ballPosDict.Add("pos18", ballPos18);

        dictLength = sphRotDict.Count;
        Debug.Log("Dictionary lenght is " + dictLength);

        KeyToDict = "pos0";                             // start position

        SafeSphereRotation = sphRotDict[KeyToDict];     // save base position for get acces
        SafeBallPosition = ballPosDict[KeyToDict];      // save base position for get acces
    }

    // change KeyToDict
    public void ChangeKeyToDict(string newKeyToDict)
    {
        KeyToDict = newKeyToDict;
        TakeRotAndPosFromDict();
        Debug.Log("New KeyToDict is " + KeyToDict);
    }

    // choose next safe position with key from another scripts
    public void AddOneToKey()
    {
        posCount++;
        if (posCount == dictLength)
        {
            posCount = dictLength - 1;
        }
        KeyToDict = "pos" + posCount;
        TakeRotAndPosFromDict();
        Debug.Log("keytoDict is " + KeyToDict);

    }

    // choose previos safe position with key from another scripts
    public void RemoveOneFromKey()
    {
        posCount--;
        if (posCount < 0)
        {
            posCount = 0;
        }
        KeyToDict = "pos" + posCount;
        TakeRotAndPosFromDict();
        Debug.Log("keytoDict is " + KeyToDict);
    }

    // write new position and rotate coordinates
    public void TakeRotAndPosFromDict()
    {
        SafeSphereRotation = sphRotDict[KeyToDict];
        SafeBallPosition = ballPosDict[KeyToDict];
    }
}
