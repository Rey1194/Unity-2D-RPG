using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
	public Transform attackPoint;
	public GameObject swoosh;
	
  // Start is called before the first frame update
	void Start() {
  	
  }

  // Update is called once per frame
  void Update() {
	  if (Input.GetButtonDown("Fire1")) {
	  	Attack();
	  }
  }
  
	private void Attack() {
		Instantiate(swoosh, attackPoint.position, Quaternion.identity);
	}
}
