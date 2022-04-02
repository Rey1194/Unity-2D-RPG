using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	//variables
	[SerializeField] private int moveSpeed = 1;
	//Editor reference
	[SerializeField] private Rigidbody2D playerRigidbody;
	[SerializeField] private Animator playerAnimator;

	// Start is called before the first frame update
	void Start() {
	 
	}
	// Update is called once per frame
	void Update() {
	 //Input detection
	 float horizontalMovement = Input.GetAxis("Horizontal");
	 float verticalMovement = Input.GetAxis("Vertical");
	 //Add velocity to the rigidbody multiplied by the move speed variable
	 playerRigidbody.velocity = new Vector2(horizontalMovement, verticalMovement) * moveSpeed;
	 //Change the move animation based on the movement of the blend tree
	 playerAnimator.SetFloat("MovementX", playerRigidbody.velocity.x);
	 playerAnimator.SetFloat("MovementY", playerRigidbody.velocity.y);
	 //Change the idle animation based on the last position of the blend tree
	 if (horizontalMovement == 1 || horizontalMovement == -1 || verticalMovement == 1 || verticalMovement == -1) {
		 playerAnimator.SetFloat("LastX", horizontalMovement);
		 playerAnimator.SetFloat("LastY", verticalMovement);
	 }
	}
}
