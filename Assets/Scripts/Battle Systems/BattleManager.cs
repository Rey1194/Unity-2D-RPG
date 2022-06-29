using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	public static BattleManager instance;
	private bool isBattleActive;
	[SerializeField] GameObject battleScene;
	[SerializeField] Transform[] playerPositions, enemyPositions;
	[SerializeField] BattleCharacters[] playerPrefabs, enemiesPrefabs;
  
	// Start is called before the first frame update
  void Start() {
	  instance = this;
	  DontDestroyOnLoad(this.gameObject);
  }

  // Update is called once per frame
  void Update() {
	  if (Input.GetKey(KeyCode.B)) {
	  	StartBattle(new string[]{"Enemy1", "Enemy2","Enemy3"});
	  }
  }
  
	public void StartBattle(string[] enemiesToSpawn) {
		if (!isBattleActive) {
			isBattleActive = true;
			GameManager.instance.battleIsActive = true;
			this.transform.position = new Vector3(
				Camera.main.transform.position.x,
				Camera.main.transform.position.y,
				this.transform.position.z
			);
			battleScene.SetActive(true);
		}
	}
}
