using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserMove : MonoBehaviour
{
    [SerializeField]
    int direction = 1;
    float speed;
    Vector3 v;
    // Start is called before the first frame update
    void Start()
    {
        v = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        speed = Random.Range(100f,120f);

        v.y += direction * Time.deltaTime * speed;
        if (transform.position.y > 35f)
        {
            direction = -1;
        }
        if (transform.position.y < 21f){
            direction = 1;
        }
        

        transform.position = v;
    }
}
