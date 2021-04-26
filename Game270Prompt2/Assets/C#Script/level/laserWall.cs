using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserWall : MonoBehaviour
{
    //-235, 10.5, 220
    //-170, 10.5, 220
    float speed = 30f;
    Vector3 v;

    // Start is called before the first frame update
    void Start()
    {
        v = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        v.x += speed * Time.deltaTime;

        if(v.x > -175)
        {
            v.x = -235;
        }

        transform.position = v;
    }
}
