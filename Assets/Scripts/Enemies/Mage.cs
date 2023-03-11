using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
	public Transform playerPos;
	public Transform enemyPos;
	public Transform bulletPoint;
	public float playerDistance;
	public float shootCadency = 15f;
	private float distance;
	//private float waitShoot = 0;
	
	public GameObject fireBall;
	
    // Start is called before the first frame update
    void Start()
    {
	    playerPos = GameObject.Find("Samurai").GetComponent<Transform>();
	    enemyPos = this.GetComponent<Transform>();
    }

    // Update is called once per frame
	void FixedUpdate()
	{
		// IF THE DISTANCE IS GREATER
		// TRANSPORT
		// WAIT A SECOND
		// IF THE DISTANCE IS NEAR
		// SHOOT
		// WAIT A SECOND AND LOOP
		
		//  crear un timer y llamar a la rutina dependiendo de la distancia
		StartCoroutine(Teleport(2.0f));
    }
	// Transportarse cerca del jugador
	private IEnumerator Teleport(float waitTime) 
	{
		// posicion del jugador
		distance = Vector3.Distance(playerPos.position, enemyPos.position);		
		// Transportarse despues de unos segundos
		yield return new WaitForSeconds(waitTime);
		this.transform.position = new Vector3(
			playerPos.position.x - 3f, 
			this.transform.position.y, 
			this.transform.position.z
		);
	}
	// Atacar al jugador
	private IEnumerator Attack(float waitForShoot)
	{
		yield return new WaitForSeconds(waitForShoot);
		// Crear cadencia de tiro
		Instantiate(fireBall, bulletPoint.transform.position, Quaternion.identity);
	}
}
