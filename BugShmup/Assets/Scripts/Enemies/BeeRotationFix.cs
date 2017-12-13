using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeRotationFix : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (transform.rotation.x != -90)
        {
            transform.localEulerAngles = new Vector3(-90, 180, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
