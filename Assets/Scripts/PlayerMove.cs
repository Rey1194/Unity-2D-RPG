using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	//Singleton
	public static PlayerMove instance;
	//variables
	[SerializeField] private int moveSpeed = 1;
	//Editor reference
	[SerializeField] private Rigidbody2D playerRigidbody;
	[SerializeField] private Animator playerAnimator;
	[SerializeField] public string transitionName;

	// Start is called before the first frame update
	void Start() {
		//check if the instance already exist
		if (instance != null && instance != this) {
			// if exist destroy
			Destroy(this.gameObject);
		}
		else {
			// if not make this the instance
			instance = this;
		}
	 // Avoid to destroy this game object
	 DontDestroyOnLoad(this.gameObject);
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
