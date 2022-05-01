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
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start() {
		instance = this;
	}
	// Method to trigger the animation
	public void FadeImage() {
		// Get the animator component an trigger the animation
		imageToFade.GetComponent<Animator>().SetTrigger("Fade_Start");
	}
}
