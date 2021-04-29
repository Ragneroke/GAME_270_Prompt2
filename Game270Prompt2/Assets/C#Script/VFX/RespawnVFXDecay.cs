using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnVFXDecay : MonoBehaviour
{
	[SerializeField]
	private float decayTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,decayTime);
    }
}
