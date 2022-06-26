﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
	public ItemManager itemOnButton;
  
	// Press Button Method
	public void Press() {
		// check if the menu is active
		if (MenuManager.instance.menuPanel.activeInHierarchy == true) {
			// Set the text of the item button the same as the item name
			MenuManager.instance.itemName.text = itemOnButton.itemName;
			// Set the text description the same as the item description
			MenuManager.instance.itemDescription.text = itemOnButton.itemDescription;
			// set the item as the item pressed button
			MenuManager.instance.activeItem = itemOnButton;
		}
		
		if (ShopManager.instance.shopMenu.activeInHierarchy == true) {
			if (ShopManager.instance.buyPanel.activeInHierarchy) {
				
			}
			else if (ShopManager.instance.sellPanel.activeInHierarchy) {
				
			}
		}
	}
}
