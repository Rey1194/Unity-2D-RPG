using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
	private Rigidbody2D enemyRB;
	private bool _isGrounded;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private float groundCheckRadio = 0.025f;
	[SerializeField] private float moveSpeed = 2f;
	
    // Start is called before the first frame update
    void Start()
    {
	    enemyRB = this.GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    { 
        
    }
	// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	protected void FixedUpdate()
	{
		_isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadio, groundLayer);
		
		if (_isGrounded)
		{
			enemyRB.velocity = new Vector2(moveSpeed, 0f);
		}
	}
}
