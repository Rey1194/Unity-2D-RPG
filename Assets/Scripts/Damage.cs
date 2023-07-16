using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
	public string enemyTag;
    [SerializeField] private float hitStopTime;

	// Sent when another object enters a trigger collider attached to this object (2D physics only).
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == enemyTag)
		{
            //GameManager.instance.Stop(hitStopTime);
            FindObjectOfType<GameManager>().Stop(hitStopTime); // sin convertirlo a singleton
			Destroy(other.gameObject);
		}
	}
}
