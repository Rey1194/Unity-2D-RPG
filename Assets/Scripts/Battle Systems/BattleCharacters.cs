﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacters : MonoBehaviour
{
	[SerializeField] bool isPlayer;
	[SerializeField] string[] attacksAviable;
	public string characterName;
	public int currentHP, currentMana, maxMana, maxHP, dexterity, defence, wpnPower, armorDefence;
	public bool isDead;
	
  // Start is called before the first frame update
  void Start() {
    
  }

  // Update is called once per frame
  void Update() {
    
  }
}
