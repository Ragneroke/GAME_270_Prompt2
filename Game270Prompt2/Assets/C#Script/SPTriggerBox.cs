using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPTriggerBox : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform spawnPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        if(other.tag == "Player")
        {
            var ch3d = other.GetComponent<CharacterController3D>();
            
            ch3d.resetPoint = spawnPoint;
        }
    }
}
