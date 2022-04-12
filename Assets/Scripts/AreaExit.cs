using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Scene manager
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
	[SerializeField] private string sceneToLoad;
	[SerializeField] private string transitionName;
	[SerializeField] AreaEnter theAreaEnter;
  // Start is called before the first frame update
  void Start()
  {
	  theAreaEnter.transitionAreaName = this.transitionName;
  }

  // Update is called once per frame
  void Update()
  {
      
  }
	// Sent when another object enters a trigger collider attached to this object (2D physics only).
	protected void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player")) {
			Debug.Log("Area exit");
			PlayerMove.instance.transitionName = this.transitionName;
			SceneManager.LoadScene(sceneToLoad);
		}
	}
}
