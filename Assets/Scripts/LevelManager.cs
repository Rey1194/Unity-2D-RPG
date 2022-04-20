using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
	[SerializeField] Tilemap tileMap;
	private Vector3 bottomLeftEdge;
	private Vector3 topRightEdge;
  // Start is called before the first frame update
  void Start() {
	  // Adjust the Limits of the level
	  bottomLeftEdge = tileMap.localBounds.min + new Vector3(0.5f, 0.5f, 0);
	  topRightEdge = tileMap.localBounds.max + new Vector3(-0.5f, -0.5f, 0);
	  // Call the method for the player instance
	  PlayerMove.instance.SetLimit(bottomLeftEdge, topRightEdge);
  }

  // Update is called once per frame
  void Update() {
      
  }
}
