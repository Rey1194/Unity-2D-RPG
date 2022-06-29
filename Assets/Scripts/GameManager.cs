using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Singleton
	public static GameManager instance;
	// Variables
	[SerializeField] PlayerStats[] playerStats;
	public bool gameMenuOpen, dialogBoxOpen, shopOpened, battleIsActive;
	public int coins;
	
  // Start is called before the first frame update
	void Start() {
		// Avoid to duplicate the game manager
  	if (instance != null && instance != this) {
  		Destroy(this.gameObject);
  	}
  	else {
  		instance = this;
  	}
  	DontDestroyOnLoad(this.gameObject);
		// Find the Player stats in the scene
  	playerStats = FindObjectsOfType<PlayerStats>();
  }

  // Update is called once per frame
	void Update() {
		// if the menu or the dialog box is open
		if (gameMenuOpen || dialogBoxOpen || shopOpened || battleIsActive) {
			// player can´t move
	  	PlayerMove.instance.deactivateMovement = true;
		}
		else {
			// Player can move
			PlayerMove.instance.deactivateMovement = false;
		}
	}
	// Method to export the player stats
	public PlayerStats[] GetPlayerStats() {
		// Return the player stats
		return playerStats;
	}
}
