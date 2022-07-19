using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisYrotate : MonoBehaviour
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
        t.rotation = Quaternion.Euler(0, target.transform.eulerAngles.y, 0);
    }
}
