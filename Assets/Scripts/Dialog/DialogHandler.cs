using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHandler : MonoBehaviour
{
	// variables
	[SerializeField] private string[] sentences;
	[SerializeField] private bool canActivateBox;
	
  // Start is called before the first frame update
  void Start() {
    
  }

  // Update is called once per frame
  void Update() {
	  if ( canActivateBox == true && Input.GetButtonDown("Fire1") && !DialogController.instance.isDialogBoxActive() ) {
	  	Debug.Log("dialog box active");
	  	DialogController.instance.ActivateDialogBox(sentences);
	  }
  }
  
	// Sent when another object enters a trigger collider attached to this object (2D physics only).
	protected void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			canActivateBox = true;
		}
	}
	// Sent when another object leaves a trigger collider attached to this object (2D physics only).
	protected void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			canActivateBox = false;
		}
	}
}
