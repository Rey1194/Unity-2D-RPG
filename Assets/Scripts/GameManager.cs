using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Singleton
	public static GameManager instance;
	// Variables
	[SerializeField] PlayerStats[] playerStats;
	public bool gameMenuOpen, dialogBoxOpen;
	
  // Start is called before the first frame update
  void Start() {
  	if (instance != null && instance != this) {
  		Destroy(this.gameObject);
  	}
  	else {
  		instance = this;
  	}
  	DontDestroyOnLoad(this.gameObject);
  	// Find the references in the editor
  	playerStats = FindObjectsOfType<PlayerStats>();
  }

  // Update is called once per frame
	void Update() {
		// if the menu or the dialog box is open
		if (gameMenuOpen || dialogBoxOpen) {
			// player can´t move
	  	PlayerMove.instance.deactivateMovement = true;
		}
		else {
			PlayerMove.instance.deactivateMovement = false;
		}
	}
  
	public PlayerStats[] GetPlayerStats() {
		return playerStats;
	}
}
