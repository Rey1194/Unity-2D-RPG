using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
  // Start is called before the first frame update
  void Start() {
	  StartCoroutine(SelfDestruct());
  }

  // Update is called once per frame
  void Update() {
    
  }
  
	private IEnumerator SelfDestruct() {
		yield return new WaitForSeconds(10.0f);
		Destroy(this.gameObject);
	}
}
