using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Reference the UI
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	// Create an instance
	public static MenuManager instance;
	// Editor's reference
	[SerializeField] private Image imageToFade;
	[SerializeField] private GameObject menuPanel;
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start() {
		instance = this;
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update() {
		// check for input and the menu is not active
		if (Input.GetKeyDown(KeyCode.M) && menuPanel.activeInHierarchy == false) {
			// Activate the menu 
			menuPanel.SetActive(true);
		}
		// check for input and the menu is active
		else if (Input.GetKeyDown(KeyCode.M) && menuPanel.activeInHierarchy == true) {
			// Deactivate the menu
			menuPanel.SetActive(false);
		}
	}
	
	// Method to trigger the animation
	public void FadeImage() {
		// Get the animator component an trigger the animation
		imageToFade.GetComponent<Animator>().SetTrigger("Fade_Start");
	}
}
