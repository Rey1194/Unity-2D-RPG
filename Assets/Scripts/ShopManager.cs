using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
	// instance of this class
	public static ShopManager instance;
	// shop panel's reference
	public GameObject shopMenu, buyPanel, sellPanel;
	[SerializeField] private TextMeshProUGUI currentCoins;
	// List of items
	public List<ItemManager> itemForSale;
	
	
	// Start is called before the first frame update
	void Start() {
		instance = this;
	}
	
	// Update is called once per frame
	void Update() {

	}
	
	public void OpenShopMenu() {
		shopMenu.SetActive(true);
		GameManager.instance.shopOpened = true;
		currentCoins.text = "Coins: " + GameManager.instance.coins;
		buyPanel.SetActive(true);
	}
	
	public void CloseShopMenu() {
		shopMenu.SetActive(false);
		GameManager.instance.shopOpened = false;
	}
	
	public void OpenBuyPanel() {
		buyPanel.SetActive(true);
		sellPanel.SetActive(false);
	}
	
	public void OpenSellPanel() {
		buyPanel.SetActive(false);
		sellPanel.SetActive(true);
	}
}
