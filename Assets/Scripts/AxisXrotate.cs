using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisXrotate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform t;
    [SerializeField] GameObject target;
    void Start()
    {
        t = transform;
    }

    // Update is called once per frame
    void Update()
    {
        t.rotation = Quaternion.Euler(target.transform.eulerAngles.x, target.transform.eulerAngles.y, 0);
    }
}
