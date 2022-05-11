using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Refernce UI
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
	// stats variables
	[SerializeField] Sprite characterImage;
	[SerializeField] int maxLevel = 50;
	[SerializeField] string playerName;
	[SerializeField] int playerLevel = 1;
	[SerializeField] int currentXP;
	[SerializeField] int[] xpForEachLevel;
	[SerializeField] int baselevelXp;
	[SerializeField] int maxHP;
	[SerializeField] int currentHP;
	[SerializeField] int maxMana;
	[SerializeField] int currentMana;
	[SerializeField] int dexterity;
	[SerializeField] int defence;

  // Start is called before the first frame update
  void Start() {
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
		}
	}
}
