using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// reference the UI
using UnityEngine.UI;
// reference Text Mesh Pro
using TMPro;

public class DialogController : MonoBehaviour
{
	// Editor's Reference
	[SerializeField] TextMeshProUGUI dialogText, nameText;
	[SerializeField] GameObject dialogBox, nameBox;
	// variables
	[SerializeField] string[] dialogSentence;
	[SerializeField] int currentSentence;
	
  // Start is called before the first frame update
	void Start() {
		// set the sentences based on the array index
	  dialogText.text = dialogSentence[currentSentence];
  }

  // Update is called once per frame
  void Update() {
	  // Check if the dialog box is active
	  if (dialogBox.activeInHierarchy) {
	  	// check if click the fire button
	  	if (Input.GetButtonUp("Fire1")) {
	  		// increase the current sentence value
	  		currentSentence++;
	  		// if currente sentence is greater than dialog sentences
	  		if (currentSentence >= dialogSentence.Length) {
	  			// deactivate the dialog box
	  			dialogBox.SetActive(false);
	  		}
	  		else {
		  		// set the sentences based on the array index
		  		dialogText.text = dialogSentence[currentSentence];
	  		}
	  	}
	  }
  }
}
