using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	// Singleton pattern
	public static Inventory instance;
	// Create a list of items
	private List<ItemManager> itemList;
	
  // Start is called before the first frame update
	void Start() {
		// Set this to inventory
		instance = this;
  	// Instantiate the list items
		itemList = new List<ItemManager>();
  }

	// Add item to the inventory
	public void AddItem(ItemManager item) {
		// check if the item is stackable
		if (item.isStackable) {
			// boolean to check if the item is on the inventory
			bool isAlreadyInInventory = false;
			// search the item in the item list of the item manager
			foreach (ItemManager itemInventory in itemList) {
				// if the name is the same
				if (itemInventory.itemName == item.itemName) {
					// stack the item
					itemInventory.amount += item.amount;
					// change the bool to true
					isAlreadyInInventory = true;
				}
			}
			// if not in the inventory
			if (!isAlreadyInInventory) {
				// Add the item to the item list
				itemList.Add(item);
			}
		}
		// if not stackable 
		else {
			// Add item tho the item list
			itemList.Add(item);
		}
	}
	
	// Remove Item form the inventory
	public void RemoveItem(ItemManager item){
		// check if is stackcable
		if (item.isStackable) {
			// keep track of the item to remove
			ItemManager inventoryItem = null;
			// make a search in the inventory
			foreach (ItemManager itemInInventory in itemList) {
				// check if the item has the same mane as the item list
				if (itemInInventory.itemName == item.itemName) {
					// reduce its value by 1
					Debug.Log("itemdiscarted");
					itemInInventory.amount--;
					// set the tracked item in the list as the item manager list
					inventoryItem = itemInInventory;
				}
			}
			// if not null and the amount is less than 0
			if (inventoryItem != null && inventoryItem.amount <= 0){
				// Remove from the list
				itemList.Remove(inventoryItem);
			}
		}
		// if not stackable
		else {
			// remove the item directly
			itemList.Remove(item);
		}
	}
	
	// Export the item list
	public List<ItemManager> GetItemList() {
		return itemList;
	}
}
