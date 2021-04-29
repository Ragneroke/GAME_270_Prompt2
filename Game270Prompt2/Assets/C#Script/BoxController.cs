using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    [SerializeField] private float pickRange = 2f;
    public bool onHand = true;
    void Start()
    {
        player = GameObject.Find("Player 0.2").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!onHand)
        {
            PickupUpdate();
            // if (transform.localScale.x < 30){
            //     transform.localScale += new Vector3(3,3,3);
            // }
            
        } else {
            // if (transform.localScale.x > 30){
            //     transform.localScale -= new Vector3(10,10,10);
            // }
        }

        ReturnHand();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag != "Player")
        {
            onHand = false;
        }


        if(other.gameObject.tag == "killPlayer")
        {
            BackToHand();
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.transform.tag != "Player")
        {
            onHand = false;
        }


        if(other.gameObject.tag == "killPlayer")
        {
            BackToHand();
        }
    }

    void PickupUpdate()
    {
        var dis = Vector3.Distance(transform.position, player.position);
        if(dis <= pickRange && !onHand)
        {
            BackToHand();
        }
    }

    void BackToHand()
    {
        var pos = player.transform.Find("Camera").transform.Find("Hand").position;
        transform.position = pos;
        transform.SetParent(player.transform.Find("Camera").transform.Find("Hand"));
        GetComponent<Rigidbody>().isKinematic = true;
        onHand = true;
    }

    void ReturnHand()
    {
        if(transform.position.y <= -70f)
        {
            BackToHand();
        }
    }
}
