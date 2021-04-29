using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ceilingLaser : MonoBehaviour
{
    public float cd;
    public float timer = 2f;
    public float shootTime = 1f;
    Transform conelaser;

    // Start is called before the first frame update
    void Start()
    {
        conelaser = this.transform.Find("Conelaser");
        Debug.Log(this.name);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            shootTime -= Time.deltaTime;
            if (shootTime < 0)
            {
                timer = cd;
                shootTime = 1f;
            }
            
            if (this.name == "laser-top")
            {
                conelaser.localScale = new Vector3(7,10,7);
            }
            else {
                conelaser.localScale = new Vector3(2,10,2);
            }
            
        }
        else {
            conelaser.localScale = new Vector3(0,0,0);
        }
    }
}
