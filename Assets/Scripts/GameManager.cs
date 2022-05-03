using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] PlayerStats[] playerStats;
	public static GameManager instance;
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
    
  }
}
