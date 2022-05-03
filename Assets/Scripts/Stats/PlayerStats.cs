using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	// stats variables
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
	  xpForEachLevel[1] = baselevelXp;
	  for (int i = 2; i < xpForEachLevel.Length; i++) {
	  	xpForEachLevel[i] = Mathf.FloorToInt(0.02f * i * i * i + 3.06f * i * i + 105.6f * i);
	  }
  }

  // Update is called once per frame
	void Update() {
  	
  }
	public void AddExp(int amountOfExp) {
		currentXP += amountOfExp;
		if (currentXP >= xpForEachLevel[playerLevel]) {
			currentXP -= xpForEachLevel[playerLevel];
			playerLevel++;
		}
	}
}
