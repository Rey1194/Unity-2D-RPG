using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Cinemachine reference
using Cinemachine;

public class CameraController : MonoBehaviour
{
	//Editor Reference
	private PlayerMove playerTarget;
	private CinemachineVirtualCamera virtualCamera;
	
  // Start is called before the first frame update
	void Start() {
		// Find the player in the scene
		playerTarget = FindObjectOfType<PlayerMove>();
		//Find the virtual camera in the scene
		virtualCamera = this.GetComponent<CinemachineVirtualCamera>();
		// Select the follow property of the virtual camera and asign the player
		virtualCamera.Follow = playerTarget.transform;
  }

  // Update is called once per frame
  void Update() {
    
  }
}
