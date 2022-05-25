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
		print(item + " Has been added");
		// Add item tho the item Manager
		itemList.Add(item);
		// check the item list to check if the item hsa been added
		print(itemList.Count);
	}
	
	// Export the item list
	public List<ItemManager> GetItemList() {
		return itemList;
	}
}
