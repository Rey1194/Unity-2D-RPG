using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	public static BattleManager instance;
	private bool isBattleActive;
	[SerializeField] GameObject battleScene;
	[SerializeField] List<BattleCharacters> activeCharacters = new List<BattleCharacters>();
	[SerializeField] Transform[] playerPositions, enemyPositions;
	[SerializeField] BattleCharacters[] playerPrefabs, enemiesPrefabs;
  
	// Start is called before the first frame update
  void Start() {
	  instance = this;
	  DontDestroyOnLoad(this.gameObject);
  }

  // Update is called once per frame
  void Update() {
	  if (Input.GetKey(KeyCode.B) && !isBattleActive) {
	  	Debug.Log("Battle start");
	  	StartBattle(new string[]{"Enemy1", "Enemy2","Enemy3"});
	  }
  }
  
	public void StartBattle(string[] enemiesToSpawn) {
		// se llama al método de configurar batalla
		SettingUpBattle();
		// Llama al método que añade a los players
		AddingPlayer();
		// Añade los enemigos
		for(int i = 0; i < enemiesToSpawn.Length; i++ ) {
			if (enemiesToSpawn[i] != "") {
				for (int j = 0; j < enemiesPrefabs.Length; j++) {
					if (enemiesPrefabs[j].characterName == enemiesToSpawn[i]) {
						BattleCharacters newEnemy = Instantiate(
							enemiesPrefabs[j],
							enemyPositions[i].position,
							enemyPositions[i].rotation,
							enemyPositions[i]
						);
						activeCharacters.Add(newEnemy);
					}
				}
			}
		}
	}
	
	
	private void AddingPlayer() {
		// Buscar a todos los personajes que tengan playerstats 
		for (int i = 0; i < GameManager.instance.GetPlayerStats().Length; i++ ) {
			// Ver si el playerstat está activo en la escena
			if (GameManager.instance.GetPlayerStats()[i].gameObject.activeInHierarchy) {
				// Buscar en los player prefabs de este manager
				for (int j = 0; j < playerPrefabs.Length; j++ ) {
					// si el nombre de los players en la escena coinciden con los de este prefabs
					if (playerPrefabs[j].characterName == GameManager.instance.GetPlayerStats()[i].playerName) {
						// Se crea un nuevo battle character
						BattleCharacters newPlayer = Instantiate(
							playerPrefabs[j],
							playerPositions[i].position,
							playerPositions[i].rotation,
							playerPositions[i]
						);
						// y se lo añade a la lista de los players
						activeCharacters.Add(newPlayer);
						// Llama al método que añade las stats
						ImportPlayerStats(i);
					}
				}
			}
		}
	}
	
	// Añadir las stats de cada player 
	private void ImportPlayerStats(int i) {
		PlayerStats player = GameManager.instance.GetPlayerStats()[i];
		activeCharacters[i].currentHP = player.currentHP;
		activeCharacters[i].currentMana = player.currentMana;
		activeCharacters[i].maxHP = player.maxHP;
		activeCharacters[i].maxMana = player.maxMana;
		activeCharacters[i].dexterity = player.dexterity;
		activeCharacters[i].defence = player.defence;
		activeCharacters[i].wpnPower = player.weaponPower;
		activeCharacters[i].armorDefence = player.armorDefence;
	}
	
	// Método para configurar la batalla
	private void SettingUpBattle() {
		// verifica que la escena de batalla esté activa
		if (!isBattleActive) {
			isBattleActive = true;
			GameManager.instance.battleIsActive = true;
			// mueve la cámara a la posición donde se encuentre la batalla
			this.transform.position = new Vector3 (
				Camera.main.transform.position.x,
				Camera.main.transform.position.y,
				this.transform.position.z
			);
			// se activa la escena de batalla
			battleScene.SetActive(true);
		}
	}
}
