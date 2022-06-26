using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Reference the UI
using UnityEngine.UI;
// Reference text mesh pro
using TMPro;

public class MenuManager : MonoBehaviour
{
	// Create an instance
	public static MenuManager instance;
	// Editor's reference
	[SerializeField] private Image imageToFade;
	[SerializeField] public GameObject menuPanel;
	[SerializeField] private GameObject[] statsButtons;
	[SerializeField] private GameObject[] characterPanel;
	// player stats references in main menu
	[SerializeField] private TextMeshProUGUI[] nameText, hpText, manaText, xpForNextLevel, xpText;
	[SerializeField] private Image[] playerImage;
	[SerializeField] private Slider[] xpSlider;
	// Player stats reference in stas panel
	[SerializeField] private Image characterStatImage;
	[SerializeField] private TextMeshProUGUI statName, statHP, statMana, statDex, statDef, statEquippedWeapon, statEquippedArmor;
	[SerializeField] private TextMeshProUGUI statWeaponPower, statArmorDefence;
	// Display items panels reference
	[SerializeField] private GameObject itemSlotContainer;
	[SerializeField] private Transform itemSlotContainerParent;
	// Character choise reference
	[SerializeField] private GameObject characterChoicePanel;
	[SerializeField] private TextMeshProUGUI[] characterChoiceName;
	// Miscelaneous variables & references
 	private PlayerStats[] playerStats;
	public  TextMeshProUGUI itemName, itemDescription;
	public ItemManager activeItem;
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start() {
		// Set the instance to this
		instance = this;
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update() {
		// check for input and the menu is not active
		if (Input.GetKeyDown(KeyCode.M) && menuPanel.activeInHierarchy == false) {
			// Update the player stats
			UpdatePlayerStats();
			// Activate the menu
			menuPanel.SetActive(true);
			// player can't move
			GameManager.instance.gameMenuOpen = true;
		}
		// check for input and the menu is active
		else if (Input.GetKeyDown(KeyCode.M) && menuPanel.activeInHierarchy == true) {
			// Deactivate the menu
			menuPanel.SetActive(false);
			// Close the menu
			GameManager.instance.gameMenuOpen = false;
		}
	}
	
	// Update Player stats
	public void UpdatePlayerStats(){
		// Get the current player stats from the game manager
		playerStats = GameManager.instance.GetPlayerStats();
		//activate character panels
		for (int i = 0; i < playerStats.Length; i++) {
			// Updathe the player UI based on the stats
			characterPanel[i].SetActive(true);
			// set the player sprite 
			playerImage[i].sprite = playerStats[i].characterImage;
			// set the player name
			nameText[i].text = playerStats[i].playerName;
			// set the player current hp
			hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
			// set the player current Mana
			manaText[i].text = "Mana: " + playerStats[i].currentMana + "/" + playerStats[i].maxMana;
			// set the player current XP
			xpText[i].text = "Current XP: " + playerStats[i].currentXP;
			// set the player xp for next level
			xpForNextLevel[i].text = playerStats[i].currentXP.ToString() + "/" + playerStats[i].xpForEachLevel[playerStats[i].playerLevel];
			// set the slider max xp for level up
			xpSlider[i].maxValue = playerStats[i].xpForEachLevel[playerStats[i].playerLevel];
			// set the slider current xp for level up
			xpSlider[i].value = playerStats[i].currentXP;
		}
	}
	
	// Find the gameobjects with stats script
	public void StatsMenu() {
		// loop for searching the stats gameobjects
		for (int i = 0; i < playerStats.Length; i++) {
			statsButtons[i].SetActive(true);
			// set the player name
			statsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerStats[i].playerName;
		}
		// Update the stats menu
		StatsMenuUpdate(0);
	}
	
	// Update stats menu panel
	public void StatsMenuUpdate(int playerSelectedNumber) {
		// store the playerstat in a variable
		PlayerStats playerSelected = playerStats[playerSelectedNumber];
		// set the character name
		statName.text = playerSelected.playerName;
		// set the character hp
		statHP.text = playerSelected.currentHP.ToString() + "/" + playerSelected.maxHP;
		// set the character mana
		statMana.text = playerSelected.currentMana.ToString() + "/" + playerSelected.maxMana;
		// set the caracter dexterity
		statDex.text = playerSelected.dexterity.ToString();
		// set the character defense
		statDef.text = playerSelected.defence.ToString();
		// set the character sprite image
		characterStatImage.sprite = playerSelected.characterImage;
		// Set the equiped weapon name
		statEquippedWeapon.text = playerSelected.equippedWeaponName;
		// Set the equipped armor name
		statEquippedArmor.text = playerSelected.equippedArmorName;
		// set the power text in the panel
		statWeaponPower.text = playerSelected.weaponPower.ToString();
		// set the defence in the panel
		statArmorDefence.text = playerSelected.armorDefence.ToString();
	}
	
	// Update the items inventory Panel
	public void UpdateInventoryPanel() {
		// Destroy the items on list before update
		foreach (Transform itemSlot in itemSlotContainerParent) {
			// Destroy the items from the canvas 
			Destroy(itemSlot.gameObject);
		}
		// Get the item list from the inventory
		foreach (ItemManager item in Inventory.instance.GetItemList()) {
			// Instantiate the item in the rectransform of the item slot from the inventory
			RectTransform itemSlot = Instantiate( itemSlotContainer, itemSlotContainerParent ).GetComponent<RectTransform>();
			// Find the item image
			Image itemImage = itemSlot.Find("ItemImage").GetComponent<Image>();
			// Set the image of the menu item from the pickup item
			itemImage.sprite = item.itemsImage;
			// Set the text of the menu item form the pickuo item
			TextMeshProUGUI itemAmountText = itemSlot.Find("ItemText").GetComponent<TextMeshProUGUI>();
			// check if the item is greater than one
			if (item.amount > 1) {
				// convert the amount text to string and display
				itemAmountText.text = item.amount.ToString();
			}
			else {
				// not show the text
				itemAmountText.text = "";
			}
			// change the text based of the item button pressed
			itemSlot.GetComponent<ItemButton>().itemOnButton = item;
		}
	}
	
	// Discard Item method
	public void DiscardItem() {
		Inventory.instance.RemoveItem(activeItem);
		UpdateInventoryPanel();
	}
	
	//Use Item method
	public void UseItem(int selectedCharacter) {
		activeItem.UseItem(selectedCharacter);
		OpenCharacterChoicePanel();
		DiscardItem();
	}
	
	// Active character choice method
	public void OpenCharacterChoicePanel() {
		// Active the character choice panel
		characterChoicePanel.SetActive(true);
		// if we have an active item
		if (activeItem) {
			// Search for the all players characters in scene
			for (int i = 0; i < playerStats.Length; i++) {
				// save the players in variable
				PlayerStats activePlayer = GameManager.instance.GetPlayerStats()[i];
				// change the button text for the player name
				characterChoiceName[i].text = activePlayer.playerName;
				// if there one or more players in scene
				bool activePlayerAviable = activePlayer.gameObject.activeInHierarchy;
				// activate the choice button
				characterChoiceName[i].transform.parent.gameObject.SetActive(activePlayerAviable);
			}
		}
	}
	
	// Deactivate the character choice menu
	public void CloseCharacterChoicePanel() {
		characterChoicePanel.SetActive(true);
	}
	
	// Quit the game
	public void QuitGame() {
		// Log for check
		Debug.Log("Quitting Game");
		//Close the game
		Application.Quit();
	}
	
	// Method to trigger the animation
	public void FadeImage() {
		// Get the animator component an trigger the animation
		imageToFade.GetComponent<Animator>().SetTrigger("Fade_Start");
	}
}
