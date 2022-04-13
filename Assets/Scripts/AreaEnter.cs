using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnter : MonoBehaviour
{
	public string transitionAreaName;
  // Start is called before the first frame update
  void Start()
  {
	  if (transitionAreaName == PlayerMove.instance.transitionName) {
	  	// move the player position to this position
	  	PlayerMove.instance.transform.position = this.transform.position;
	  }
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
