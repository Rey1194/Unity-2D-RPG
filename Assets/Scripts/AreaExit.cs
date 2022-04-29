using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Scene manager
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
	// Variables
	[SerializeField] private string sceneToLoad;
	[SerializeField] private string transitionName;
	[SerializeField] AreaEnter theAreaEnter;
	
  // Start is called before the first frame update
  void Start() {
	  theAreaEnter.transitionAreaName = this.transitionName;
  }

	// Sent when another object enters a trigger collider attached to this object (2D physics only).
	protected void OnTriggerEnter2D(Collider2D other)
	{
		// Check if collide with the player
		if (other.CompareTag("Player")) {
			// trigger the fade_start animation
			MenuManager.instance.FadeImage();
			// Set the transition name to this new transition name
			PlayerMove.instance.transitionName = this.transitionName;
			// Invoke the Coroutine
			StartCoroutine(LoadSceneCoroutine());
		}
	}
	// IEnumerator to wait for the change of scene
	IEnumerator LoadSceneCoroutine() {
		// wait for one second
		yield return new WaitForSeconds(1f);
		// then load the new scene
		SceneManager.LoadScene(sceneToLoad);
	}
}
