using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	//variables
	public enum ItemType { Weapon, Armor, Item }
	public enum AffectType { HP, Mana }
	public ItemType itemType;
	public AffectType affectType;
	public string itemName, itemDescription;
	public Sprite itemsImage;
	public int valueInCoins;
	public int amountOfAffect;
	public int weaponDexerity;
	public int armorDefence;
	public int amount;
	public bool isStackable;
	
	
	// Use item method
	public void UseItem(int characterToUseOne) {
		// Select the caracter from the game manager
		PlayerStats selectedCharacter = GameManager.instance.GetPlayerStats()[characterToUseOne];
		// check if the item is a use item
		if (itemType == ItemType.Item) {
			// check if the use item is a health potion
			if (affectType == AffectType.HP) {
				// call the AddHealth method from the playerStats
				selectedCharacter.AddHealth(amountOfAffect);
			}
			// check if the use item is a mana potion
			else if (affectType == AffectType.Mana) {
				// call the AddMana method from the playerStats
				selectedCharacter.AddMana(amountOfAffect);
			}
		}
	}
  
	// Sent when another object enters a trigger collider attached to this object (2D physics only).
	protected void OnTriggerEnter2D(Collider2D other) {
		// Check if the item collide with the player
		if (other.CompareTag("Player")) {
			// Add the item to the inventory
			Inventory.instance.AddItem(this);
			// Destroy the item gameobject
			SelfDestroy();
		}
	}
	
	// Destroy the gameobject
	private void SelfDestroy() {
		//Destroy(this.gameObject);
		this.gameObject.SetActive(false);
	}
}
