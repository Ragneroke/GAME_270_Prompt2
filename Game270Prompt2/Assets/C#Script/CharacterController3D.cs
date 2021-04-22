using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
	[SerializeField] private GameObject firstCamera;
	[SerializeField] float mouseSenstivity = 3.5f;
	[SerializeField][Range(0.0f, 25.0f)] float walkSpeed = 10, runSpeed = 20;
    [SerializeField][Range(0.0f, 25.0f)] float jumpSpeed = 8.0f;
	[SerializeField][Range(0.0f, 20.0f)] float gravityStrength = 13.0f;
	[SerializeField] float moveSmoothTime = 0.1f;
	[SerializeField] float mouseSmoothTime = 0.03f;
	[SerializeField] [Range(0.0f, 5.0f)] private float leanAmount = 1.0f;
	[SerializeField] float leanSpeed = 5.0f;
	[SerializeField] bool lockCursor = true;

	float cameraPitch = 0.0f;
	float velocityY = 0.0f;

	CharacterController controller;

	Vector2 currentDir = Vector2.zero;
	Vector2 currentDirVelocity = Vector2.zero;

	Vector2 currentMouseDelta = Vector2.zero;
	Vector2 currentMouseDeltaVelocity = Vector2.zero;

	private bool isWalking;
	private Vector3 move = Vector3.zero;
	private Vector2 input;
	public bool isGrounded;
    [SerializeField]private Vector3 jumpDirection = Vector3.zero;
	
	// Might use this later on.
	// public LayerMask whatIsFloor;
    //Reset Variables
    private float resetCd = 4f;
    private float lastReset = -4f;
    [SerializeField]private Transform resetPoint;
	[SerializeField]private bool isReset = false;
    public GameObject ashBox;
	private Transform deathCamera;
	private float rotationSpeed = 5;
	Animator animator;
	
	//Throw ash box part

	private Transform hand;
	[SerializeField] private float throwForce;
	private Transform collector;

	private void Start()
	{
		controller = GetComponent<CharacterController>();
        resetPoint = GameObject.Find("SpawnPoint").transform;
		deathCamera = transform.Find("DeathCamera").transform;
		firstCamera = transform.Find("Camera").gameObject;
		hand = transform.Find("Camera").transform.Find("Hand");
		animator = transform.Find("Capsule").GetComponent<Animator>();
		collector = GameObject.Find("AshBoxCollector").transform;
		// Hide the cursor and lock mouse to screen.
		if (lockCursor)
		{
            Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	private void Update()
	{
		if(!isReset)
		{
			MouseLook();
			UpdateMovement();
        	UpdateJump();
			ThrowUrn();

		}		
	}

    private void FixedUpdate()
    {
		isGrounded = controller.isGrounded;
	}

	private void LateUpdate()
	{
		deathCamera.rotation = Quaternion.Slerp(deathCamera.rotation, Quaternion.LookRotation(transform.position - deathCamera.position), rotationSpeed*Time.deltaTime);
		UpdateReset();
	}
	// Forces the camera to follow the mouse movement.
    void MouseLook()
	{
		// Get the mouse position.
		Vector2 targeMouseDelta = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

		currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targeMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

		// Vertical and Horizontal Rotation.
		cameraPitch -= currentMouseDelta.x * mouseSenstivity;

		// Clamp camera pitch.
		cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f); 

		// Move the camera.
		playerCamera.transform.localEulerAngles = Vector3.right * cameraPitch;
		transform.Rotate(Vector3.up * currentMouseDelta * mouseSenstivity);
	}

	// Moves the character controller component and causes the player to lean, while moving.
	void UpdateMovement()
	{
		// Get input axes
		float vertical = Input.GetAxisRaw("Vertical");
		float horizontal = Input.GetAxisRaw("Horizontal");
		
		// Smooth vector target
		Vector2 targetDir = new Vector2(horizontal, vertical);
		targetDir.Normalize();

		currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

		// Lean towards direction of travel
		float x = Input.GetAxis("Vertical") * leanAmount;
		float z = Input.GetAxis("Horizontal") * leanAmount;
		Vector3 euler = transform.localEulerAngles;
		euler.z = Mathf.LerpAngle(euler.z, z, Time.deltaTime * leanSpeed);
		euler.x = Mathf.LerpAngle(euler.x, x, Time.deltaTime * leanSpeed);
		transform.localEulerAngles = euler;

		// Check if the character controller is grounded
		if (controller.isGrounded)
			velocityY = 0.0f;

		velocityY += -Mathf.Abs(gravityStrength) * Time.deltaTime; // Invert the gravity force

		// Movement vector
		Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

		controller.Move(velocity * Time.deltaTime);
	}

    void UpdateJump()
    {
        if(controller.isGrounded && Input.GetButton("Jump"))
        {
            Debug.Log(controller.isGrounded);
            jumpDirection.y = jumpSpeed;
        }

        jumpDirection.y -= gravityStrength * Time.deltaTime;
		if(jumpDirection.y <= -10f)
		{
			jumpDirection.y = -10f;
		}
        controller.Move(jumpDirection * Time.deltaTime);
    }

	private void ThrowUrn()
	{
		if(Input.GetMouseButton(0) && hand.transform.childCount != 0)
		{
			var horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);

			if(controller.isGrounded){
				throwForce = 12f;
			} else {
				throwForce = 16f;
			}
			var box = hand.transform.GetChild(0);
			box.SetParent(collector);
			box.GetComponent<Rigidbody>().isKinematic = false;
			box.GetComponent<Rigidbody>().velocity = firstCamera.transform.forward * throwForce + horizontalVelocity;
		}
	}

    void UpdateReset()
    {
        if(lastReset + resetCd < Time.time && Input.GetKey(KeyCode.R))
        {
			StartCoroutine(DeathAnimation());
			lastReset = Time.time;
        }
    }

	void CallReset()
	{
		StartCoroutine(DeathAnimation());
		lastReset = Time.time;
	}
	IEnumerator DeathAnimation(){
		controller.enabled = false;
		isReset = true;
		deathCamera.gameObject.SetActive(true);
		firstCamera.SetActive(false);
		animator.SetBool("isResetting", true);
		yield return new WaitForSeconds(2f);
		deathCamera.gameObject.SetActive(false);
		firstCamera.SetActive(true);
		animator.SetBool("isResetting", false);
		deathDealer();
		isReset = false;
		controller.enabled = true;
	}

	void deathDealer(){
		if(collector.childCount > 0)
		{
			var boxT = collector.GetChild(0);
			var targetPos = boxT.position;
			targetPos.y += 2f;
			transform.position = targetPos;
		}else{
			var pos = resetPoint.position;
        	transform.position = pos;
			
		}

	}

	private void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.gameObject.tag == "killPlayer")
		{
			CallReset();
		}
	}
}
