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
	public GameObject fireBall;
	
	// Timer
	public float timeRemaining = 2f;
	public bool timerIsRunning = false;
	
    // Start is called before the first frame update
    void Start()
	{
	    playerPos = GameObject.Find("Samurai").GetComponent<Transform>();
	    enemyPos = this.GetComponent<Transform>();
	    
	    timerIsRunning = true;
    }

    // Update is called once per frame
	void FixedUpdate()
	{
		//  CREAR DIFERENTES PUNTOS PARA TRANSPORTARSE.
		if (timerIsRunning)
		{
			if (timeRemaining > 0)
			{
				timeRemaining -= Time.deltaTime;
				if (timeRemaining <= 0){
					//  crear un timer y llamar a la rutina dependiendo de la distancia
					StartCoroutine(Teleport(2.0f));
					timeRemaining = 2f;
				}
			}
		}

    }
	// Transportarse cerca del jugador
	private IEnumerator Teleport(float waitTime) 
	{
		// posicion del jugador
		distance = Vector3.Distance(playerPos.position, enemyPos.position);		
		if (this.distance > 5 || this.distance > -5){
			// Transportarse despues de unos segundos
			yield return new WaitForSeconds(waitTime);
			this.transform.position = new Vector3(
				playerPos.position.x - 3f, 
				this.transform.position.y, 
				this.transform.position.z
			);
		}
		
		if (this.distance <= 5 || this.distance <= -5) {
			StartCoroutine(Attack(0.1f));
		}
		
		// reset timer
		timerIsRunning = false;
		yield return new WaitForSeconds(1f);
		timerIsRunning = true;
	}
	// Atacar al jugador
	private IEnumerator Attack(float waitForShoot)
	{
		yield return new WaitForSeconds(waitForShoot);
		// Crear cadencia de tiro
		Instantiate(fireBall, bulletPoint.transform.position, Quaternion.identity);
	}
	
	// Sent when an incoming collider makes contact with this object's collider (2D physics only).
	protected void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		
	}
	// Sent when a collider on another object stops touching this object's collider (2D physics only).
	protected void OnCollisionExit2D(Collision2D collisionInfo)
	{
		
	}
}
