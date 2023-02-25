using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
	public Transform playerPos;
	public Transform enemyPos;
	public float playerDistance;
	
	public GameObject fireBullet;
	
    // Start is called before the first frame update
    void Start()
    {
	    playerPos = GameObject.Find("Samurai").GetComponent<Transform>();
	    enemyPos = this.GetComponent<Transform>();
    }

    // Update is called once per frame
	void FixedUpdate()
	{
		float distance = Vector3.Distance(playerPos.position, enemyPos.position);		
		if (distance > playerDistance || distance < -playerDistance)
		{
			StartCoroutine(Teleport(2.0f));
		}
    }
	// Transportarse cerca del jugador
	private IEnumerator Teleport(float waitTime) 
	{
		yield return new WaitForSeconds(waitTime);
		this.transform.position = new Vector3(
			playerPos.position.x - 2f, 
			this.transform.position.y, 
			this.transform.position.z
		);
		Debug.Log("Transportado");
	}
	// Atacar al jugador
	private IEnumerator Attack()
	{
		// instanciar una bola de fuego que se dirija al jugador
		return null;
	}
}
