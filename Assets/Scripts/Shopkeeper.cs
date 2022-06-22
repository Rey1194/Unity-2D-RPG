using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
	// Variables
	private bool canOpenShop;
	[SerializeField] List<ItemManager> shopKeeperitemsForSale;
	
  // Start is called before the first frame update
  void Start() {
    
  }

  // Update is called once per frame
	void Update() {
		//Storage the condition in a variable for short
		bool deactivateMove = !PlayerMove.instance.deactivateMovement;
		bool shopActive = !ShopManager.instance.shopMenu.activeInHierarchy;
		// check if the shop and the movement is not active
		if (canOpenShop && Input.GetButtonDown("Fire1") && deactivateMove && shopActive) {
			// set the values form the items for sale to the shopkeeper list
			ShopManager.instance.itemForSale = this.shopKeeperitemsForSale;
			// Open the menu
			ShopManager.instance.OpenShopMenu();
	  }
  }
  
	// Sent when another object enters a trigger collider attached to this object (2D physics only).
	protected void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			canOpenShop = true;
		}
	}
	
	// Sent when another object leaves a trigger collider attached to this object (2D physics only).
	protected void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			canOpenShop = false;
		}
	}
}
