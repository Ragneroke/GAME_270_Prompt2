using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    [SerializeField] private float pickRange = 2f;
    public bool onHand = true;
    private Vector3 offHandScale;
    void Start()
    {
        player = GameObject.Find("Player 0.2").transform;
        offHandScale = new Vector3(62f,62f,62f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!onHand)
        {
            PickupUpdate();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Ground")
        {
            onHand = false;
            transform.localScale = offHandScale;
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
            transform.localScale = new Vector3(60f, 60f, 60f);
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
}
