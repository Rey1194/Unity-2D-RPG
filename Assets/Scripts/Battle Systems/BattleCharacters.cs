using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacters : MonoBehaviour
{
	// Player variables
	[SerializeField] bool isPlayer;
	[SerializeField] string[] attacksAviable;
	public string characterName;
	public int currentHP, currentMana, maxMana, maxHP, dexterity, defence, wpnPower, armorDefence;
	public bool isDead;
	
	// return the bool value
	public bool GetIsPlayer() {
		return isPlayer;
	}
}
