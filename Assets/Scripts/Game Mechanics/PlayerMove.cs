using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	//Singleton
	public static PlayerMove instance;
	//variables
	[SerializeField] private int moveSpeed = 1;
	private Vector3 bottomLeftEdge;
	private Vector3 topRightEdge;
	public bool deactivateMovement = false;
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
	 // Check if the player can move
	 if (deactivateMovement == false) {
		 //Add velocity to the rigidbody multiplied by the move speed variable
		 playerRigidbody.velocity = new Vector2(horizontalMovement, verticalMovement) * moveSpeed;
	 }
	 else {
			// Set the movement to 0;
			playerRigidbody.velocity = Vector2.zero;
	 }
	 //Change the move animation based on the movement of the blend tree
	 playerAnimator.SetFloat("MovementX", playerRigidbody.velocity.x);
	 playerAnimator.SetFloat("MovementY", playerRigidbody.velocity.y);
	 //Change the idle animation based on the last position of the blend tree
	 if (horizontalMovement == 1 || horizontalMovement == -1 || verticalMovement == 1 || verticalMovement == -1) {
		 if (!deactivateMovement) {
				playerAnimator.SetFloat("LastX", horizontalMovement);
				playerAnimator.SetFloat("LastY", verticalMovement);
		 }
	 }
	 // Avoid the player to move beyond the limits of the tiles
	 this.transform.position = new Vector3(
			Mathf.Clamp(this.transform.position.x, bottomLeftEdge.x, topRightEdge.x),
			Mathf.Clamp(this.transform.position.y, bottomLeftEdge.y, topRightEdge.y),
			Mathf.Clamp(this.transform.position.z, bottomLeftEdge.z, topRightEdge.z)
	 );
	}
	public void SetLimit(Vector3 bottomEdgeToSet, Vector3 topEdgeToSet) {
		bottomLeftEdge = bottomEdgeToSet;
		topRightEdge = topEdgeToSet;
	}
}
