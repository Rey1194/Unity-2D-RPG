using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed;
	public float wait;
	public Transform target;
	private Vector2 positionToChargeTowards;
	
    // Start is called before the first frame update
    void Start()
    {
	    target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	    // Guardar la posición del jugador
	    positionToChargeTowards = target.position;
    }

    // Update is called once per frame
	void FixedUpdate()
	{
	    //move to the player
		this.transform.position = Vector2.MoveTowards(
			this.transform.position, 
			positionToChargeTowards,
			speed * Time.deltaTime
		);
		StartCoroutine(DestroyAfterTime(wait));
	}
    
	// Sent when an incoming collider makes contact with this object's collider (2D physics only).
	protected void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		if (collisionInfo.gameObject.tag == "Player")
		{
			Destroy(this.gameObject);
		}
	}
	
	private IEnumerator DestroyAfterTime(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Destroy(this.gameObject);
	}
}
