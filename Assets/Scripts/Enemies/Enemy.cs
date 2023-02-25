using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private Rigidbody2D enemyRB;
	private string currentState;
	private Animator _animator;
	[SerializeField] private float moveSpeed = 2f;
	
    // Start is called before the first frame update
    void Start()
	{
		_animator = this.GetComponent<Animator>();
	    enemyRB = this.GetComponent<Rigidbody2D>();
    }

	void FixedUpdate()
    {
    	enemyRB.velocity = new Vector2(moveSpeed, 0f);
    	ChangeAnimationState("move");
    	//enemyRB.velocity = new Vector2 (1f, 0); // velocidad constante
    	//enemyRB.AddForce(new Vector2(100f, 0f) * Time.deltaTime); // aumenta la velocidad con el tiempo
    	//enemyRB.MovePosition(enemyRB.position + new Vector2(10f, 0f) * Time.deltaTime); // funciona mejor con el RB kinematico
    	//this.transform.Translate(new Vector2(1f, 0f)* Time.deltaTime * moveSpeed);
    	//this.transform.position = new Vector2(transform.position.x + 1f, transform.position.y);
    	//this.transform.position = Vector2.MoveTowards(transform.position, new Vector2(8f, transform.position.y), moveSpeed * Time.deltaTime);
    }
	private void ChangeAnimationState(string newState)
	{
		if (currentState == newState) return;
		_animator.Play(newState);
		currentState = newState;
	}
}
