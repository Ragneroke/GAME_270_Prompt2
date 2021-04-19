using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Base : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController2D controller;
    private SpriteRenderer sr;
    public float moveSpeed  = 40f;
    float horizontalMove = 0f;
    private int maxHealth = 100;
    private int health;
    // Damage cool down variables
    private float lastHit = 0f;
    private float hitCd = 1f;
    [SerializeField] private bool jump = false;
    [SerializeField] private bool isInvic = false;
    void Start()
    {
        health = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        
        if(Input.GetButtonDown("Jump"))
        {
            
            jump = true;
            
        }
        
    }

    void FixedUpdate() 
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
