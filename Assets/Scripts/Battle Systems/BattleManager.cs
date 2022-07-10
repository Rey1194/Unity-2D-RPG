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
	[SerializeField] int currentTurn;
	[SerializeField] bool waitingForTurn;
	[SerializeField] GameObject UIButtonHolder;
  
	// Start is called before the first frame update
  void Start() {
	  instance = this;
	  // avoid to destroy this battle manager when load
	  DontDestroyOnLoad(this.gameObject);
  }

  // Update is called once per frame
	void Update() {
		// simple input battle start
	  if (Input.GetKey(KeyCode.B) && !isBattleActive) {
	  	Debug.Log("Battle start");
	  	StartBattle(new string[]{"Enemy1", "Enemy2","Enemy3"});
	  }
		// Simple input next turn ( must change)
	  if (Input.GetKey(KeyCode.N) && isBattleActive) {
	  	NextTurn();
	  }
	  // Check buttosn Holders
	  CheckButtonsHolders();
  }
	// hide the players buttons in the enemy turns
	private void CheckButtonsHolders() {
		// if in battle
		if (isBattleActive) {
			// if the player waiting for turn
			if (waitingForTurn) {
				// if is the player turn
				if (activeCharacters[currentTurn].GetIsPlayer()) {
					// activate the player buttons actions
					UIButtonHolder.SetActive(true);
				}
				else {
					// deactivate player actions buttons
					UIButtonHolder.SetActive(false);
					// start enemy coroutine
					StartCoroutine(EnemyMoveCoroutine());
				}
			}
		}
	}
  
	//
	public void StartBattle(string[] enemiesToSpawn) {
		// Verifica que se haya iniciado una batalla
		if (!isBattleActive) {
			// se llama al método de configurar batalla
			SettingUpBattle();
			// Llama al método que añade a los players
			AddingPlayer();
			// LLama al método que añade a los enemigos
			AddingEnemies(enemiesToSpawn);
			// espera por turno
			waitingForTurn = true;
			// el turno inicial es aleatorio
			currentTurn = 0; //Random.Range(0, activeCharacters.Count);
		}	
	}
	
	// add enemies to battle
	private void AddingEnemies(string[] enemiesToSpawn) {
		// Busca y añade los enemigos
		for(int i = 0; i < enemiesToSpawn.Length; i++ ) {
			// si tiene nombre el enemigo
			if (enemiesToSpawn[i] != "") {
				// se busca en la lista de los prefabs de enemigos
				for (int j = 0; j < enemiesPrefabs.Length; j++) {
					// ve matchea los nombres de los enemigos
					if (enemiesPrefabs[j].characterName == enemiesToSpawn[i]) {
						//  se instancia los enemigos en las posiciones de la batalla
						BattleCharacters newEnemy = Instantiate(
							enemiesPrefabs[j],
							enemyPositions[i].position,
							enemyPositions[i].rotation,
							enemyPositions[i]
						);
						// se los añade a la lista de los personajes activos
						activeCharacters.Add(newEnemy);
					}
				}
			}
		}
	}
	
	// Add players to battle
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
	// Método que pasa al siguiente turno
	private void NextTurn() {
		// suma 1 al valor del turno actual
		currentTurn++;
		// si se llega al máximo
		if (currentTurn >= activeCharacters.Count) {
			// se reinician los turnos
			currentTurn = 0;
		}
		// esperan al siguiente turno
		waitingForTurn = true;
		// llama al método
		UpdateBattle();
	}
	
	// Método que verifica si los persojanes están muertos
	private void UpdateBattle() {
		// variables de verificar si están muertos los players enemies.
		bool allEnemiesAreDead = true;
		bool allPlayersAreDead = true;
		// se busca en todos los personajes activos
		for (int i = 0; i < activeCharacters.Count; i++) {
			// si la vida es menor a 0
			if (activeCharacters[i].currentHP < 0) {
				// se la setea a 0 para evitar errores
				activeCharacters[i].currentHP = 0;
			}
			// si la vida es 0 
			if (activeCharacters[i].currentHP == 0) {
				// Kill Character
			}
			else {
				// caso contrario los players siguen vivos 
				if (activeCharacters[i].GetIsPlayer()) {
					allPlayersAreDead = false;
				}
				else {
					allEnemiesAreDead = false;
				}
			}
		}
		
		// verifica si todos los jugadores o enemigos están muertos
		if (allEnemiesAreDead || allPlayersAreDead) {
			if (allEnemiesAreDead) {
				Debug.Log("all enemies dead");
			}
			else if (allPlayersAreDead) {
				Debug.Log("all players dead");
			}
			// desactiva la escena
			battleScene.SetActive(false);
			// permite al jugador moverse
			GameManager.instance.battleIsActive = false;
			// termina la batalla
			isBattleActive = false;
		}
	}
	
	// coroutina para pasar al siguiente turno
	public IEnumerator EnemyMoveCoroutine () {
		waitingForTurn = false;
		yield return new WaitForSeconds(1f);
		EnemyAttack();
		yield return new WaitForSeconds(1f);
		NextTurn();
	}
	
	// Método que controla los ataques del enemigo
	private void EnemyAttack() {
		// crea una nueva lista
		List<int> players = new List<int>();
		// añade a la lista los jugadores que tengan vida
		for (int i = 0; i < activeCharacters.Count; i++) {
			if (activeCharacters[i].GetIsPlayer() && activeCharacters[i].currentHP > 0) {
				players.Add(i);
			}
		}
		// seleciona a uno de los jugadores con vida.
		int selectedPlayerToAttack = players[Random.Range(0, players.Count)];
	}
} // fin de la clase
