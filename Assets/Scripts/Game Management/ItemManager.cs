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
	public int valueInCoins;
	public Sprite itemsImage;
	public int amountOfAffect;
	public int weaponDexerity;
	public int armorDefence;
	
  // Start is called before the first frame update
  void Start() {
	  
  }

  // Update is called once per frame
  void Update() {
    
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
		Destroy(this.gameObject);
	}
}
