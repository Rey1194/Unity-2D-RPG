using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// reference the UI
using UnityEngine.UI;
// reference Text Mesh Pro
using TMPro;

public class DialogController : MonoBehaviour
{
	// Instance
	public static DialogController instance;
	// Editor's Reference
	[SerializeField] TextMeshProUGUI dialogText, nameText;
	[SerializeField] GameObject dialogBox, nameBox;
	// variables
	[SerializeField] string[] dialogSentence;
	[SerializeField] int currentSentence;
	private bool dialogJustStarted;
	
  // Start is called before the first frame update
	void Start() {
		instance = this;
		// set the sentences based on the array index
	  dialogText.text = dialogSentence[currentSentence];
  }

  // Update is called once per frame
  void Update() {
	  // Check if the dialog box is active
	  if (dialogBox.activeInHierarchy) {
	  	// check if click the fire button
	  	if (Input.GetButtonUp("Fire1")) {
	  		//
	  		if (!dialogJustStarted) {
	  			// increase the current sentence value
		  		currentSentence++;
		  		// if currente sentence is greater than dialog sentences
		  		if (currentSentence >= dialogSentence.Length) {
			  		// deactivate the dialog box
			  		dialogBox.SetActive(false);
			  		// Allow the player to walk again
			  		PlayerMove.instance.deactivateMovement = false;
		  		}
		  		else {
			  		// change the name of the character
			  		CheckForNames();
			  		// set the sentences based on the array index
			  		dialogText.text = dialogSentence[currentSentence];
		  		}
	  		}
	  		else {
	  			// change the value
	  			dialogJustStarted = false;
	  		}
	  	}
	  }
  }
  
	// change the name based on the character
	public void CheckForNames() {
		// check if start with #
		if ( dialogSentence[currentSentence].StartsWith("#") ) {
			// Replace the name text
			nameText.text = dialogSentence[currentSentence].Replace("#", "");
			// Increase to the next sentence
			currentSentence++;
		}
	}
	
	// Activate the dialog box method
	public void ActivateDialogBox(string[] newSentenceToUse) {
		// set the dialog sentences to the new sentence
		dialogSentence = newSentenceToUse;
		// set the current sentence to the fisrt in the array
		currentSentence = 0;
		// change the name of the character
		CheckForNames();
		// change the text based on the array value
		dialogText.text = dialogSentence[currentSentence];
		// activate the dialog box game object
		dialogBox.SetActive(true);
		// change the bool value
		dialogJustStarted = true;
		// avoid the player to move when the dialog is active
		PlayerMove.instance.deactivateMovement = true;
	}
	// check if the dialogox is active
	public bool isDialogBoxActive(){
		// check if the dialog box is active in the scene
		return dialogBox.activeInHierarchy;
	}
	
}
