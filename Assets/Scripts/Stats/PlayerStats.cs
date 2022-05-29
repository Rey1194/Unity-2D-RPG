using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Refernce UI
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
	// Instance
	public static PlayerStats instance;
	// stats variables
	[SerializeField] int maxLevel = 50;
	[SerializeField] int baselevelXp;
	public Sprite characterImage;
	public string playerName;
	public int[] xpForEachLevel;
	public int defence;
	public int dexterity;
	public int playerLevel = 1;
	public int maxMana;
	public int currentMana;
	public int currentXP;
	public int currentHP;
	public int maxHP;

  // Start is called before the first frame update
	void Start() {
		// Set the instance to this
		instance = this;
		// set hte max level in the xp for each level
	  xpForEachLevel = new int[maxLevel];
	  // Set the base level to the xp for each level
	  xpForEachLevel[1] = baselevelXp;
	  // increase the xp required based on the level
	  for (int i = 2; i < xpForEachLevel.Length; i++) {
	  	// Dark souls Formula
	  	xpForEachLevel[i] = Mathf.FloorToInt(0.02f * i * i * i + 3.06f * i * i + 105.6f * i);
	  }
  }

  // Update is called once per frame
	void Update() {
  	
	}
	// Increase Health
	public void AddHealth(int amountHpToAdd) {
		// Add to the current healt the value
		currentHP += amountHpToAdd;
		// if the current health is greater than the max health
		if (currentHP >= maxHP) {
			// set the same vaule of the max health
			currentHP = maxHP;
		}
	}
	// Increase mana
	public void AddMana(int amountManaToAdd) {
		// add to the current mana value
		currentMana += amountManaToAdd;
		// if the current mana is greater than the max mana
		if (currentMana >= maxMana) {
			// set he same value of the max mana
			currentMana = maxMana;
		}
	}
	
	// Increase the xp for the level
	public void AddExp(int amountOfExp) {
		// increase the current adding the value parameter's value
		currentXP += amountOfExp;
		// if the xp is greater than the required xp to increase lvl
		if (currentXP >= xpForEachLevel[playerLevel]) {
			// reset the current xp
			currentXP -= xpForEachLevel[playerLevel];
			// increase a level
			playerLevel++;
			// Increas the dexterity or defence
			if (playerLevel % 2 == 0){
				// increase dexterity
				dexterity++;
			}
			else {
				// increase defence
				defence++;
			}
			// increase the max hp
			maxHP = Mathf.FloorToInt(maxHP * 1.18f);
			// restore max health
			currentHP = maxHP;
			//  increase max mana
			maxMana = Mathf.FloorToInt(maxMana * 1.06f);
			// Restore max mana
			currentMana = maxMana;
		}
	}
}
