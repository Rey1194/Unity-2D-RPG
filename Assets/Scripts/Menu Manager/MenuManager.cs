using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Reference the UI
using UnityEngine.UI;
// Reference text mesh pro
using TMPro;

public class MenuManager : MonoBehaviour
{
	// Create an instance
	public static MenuManager instance;
	// Editor's reference
	[SerializeField] private Image imageToFade;
	[SerializeField] private GameObject menuPanel;
	[SerializeField] private TextMeshProUGUI[] nameText, hpText, manaText, lvlText, xpText;
	[SerializeField] private Slider[] xpSlider;
	[SerializeField] private Image[] playerImage;
	[SerializeField] private GameObject[] characterPanel;
	private PlayerStats[] playerStats;
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start() {
		instance = this;
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update() {
		// check for input and the menu is not active
		if (Input.GetKeyDown(KeyCode.M) && menuPanel.activeInHierarchy == false) {
			//Update the player stats
			UpdatePlayerStats();
			// Activate the menu
			menuPanel.SetActive(true);
			// player can't move
			GameManager.instance.gameMenuOpen = true;
		}
		// check for input and the menu is active
		else if (Input.GetKeyDown(KeyCode.M) && menuPanel.activeInHierarchy == true) {
			// Deactivate the menu
			menuPanel.SetActive(false);
			GameManager.instance.gameMenuOpen = false;
		}
	}
	
	// Update Player stats
	public void UpdatePlayerStats(){
		// Get the current player stats from the game manager
		playerStats = GameManager.instance.GetPlayerStats();
		//activate character panels
		for (int i = 0; i < playerStats.Length; i++) {
			characterPanel[i].SetActive(true);
		}
	}
	
	// Method to trigger the animation
	public void FadeImage() {
		// Get the animator component an trigger the animation
		imageToFade.GetComponent<Animator>().SetTrigger("Fade_Start");
	}
}
