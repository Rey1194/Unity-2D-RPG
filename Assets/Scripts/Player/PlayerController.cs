using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static PlayerController instance;
	
	[SerializeField] private float _speed = 1.5f;
	[SerializeField] private float jumpForce = 2.5f;
	[SerializeField] private bool lookRight;
	
	private float groundCheckRadio = 0.025f;
	private bool _isGrounded;
	private string currentState;
	
	private bool canDash = true;
	private bool isDashing;
	private bool facingRight;
	private bool isAttacking = false;
	
	private float dashingPower = 14f;
	private float dashingTime = 0.2f;
	private float dashingCooldown = 1f;
	
	public LayerMask groundLayer;
	private Vector2 _movement;
	
	public Transform groundCheck;
	private Rigidbody2D _rigidbody;
	private Animator _animator;
	private TrailRenderer _trail;
	private SpriteRenderer _spriterender;
	
	private float knockBackCounter;
	private float knockBackLenght = 0.25f;
	private float knockBackForce = 5f;
	
	protected void Awake()
	{
		instance = this;
		_rigidbody = GetComponent<Rigidbody2D>();
		_trail = GetComponent<TrailRenderer>();
		_spriterender = GetComponent<SpriteRenderer>();
	}
	
	void Start()
	{
		_animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (isDashing == true) {
			return;
		}
		
		if (isAttacking == false) 
		{
			// Get the player inputs
			float horizontalInput = Input.GetAxisRaw("Horizontal");
			_movement = new Vector2(horizontalInput, 0f);
			// flip
			if (horizontalInput < 0f && !facingRight) {
				Flip();
				
			}
			else if(horizontalInput >0f && facingRight) {
				Flip();
			}
		}
		// Jump
		_isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadio, groundLayer);
		if (Input.GetButtonDown("Jump") && _isGrounded == true) {
			_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
		// Dash
		if (Input.GetKeyDown(KeyCode.LeftAlt) && canDash == true && _isGrounded == true) {
			StartCoroutine(Dash());
		}
		// wanna attack
		if (Input.GetButton("Fire1") && isAttacking == false) {
			StartCoroutine(Attack());
		}
	}
	
	protected void FixedUpdate()
	{
		if (isDashing == true) {
			return;
		}
		
		if (isAttacking == false && isDashing == false && knockBackCounter <= 0) {
			// Movement
			float horizontalVelocity = _movement.normalized.x * _speed;
			_rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
		}
		else if (isAttacking == false) {
			// knockback horizontal
			knockBackCounter -= Time.deltaTime;
			if (!_spriterender.flipX) {
				_rigidbody.velocity = new Vector2(-knockBackForce, _rigidbody.velocity.y);
			}
			else {
				_rigidbody.velocity = new Vector2(knockBackForce, _rigidbody.velocity.y);
			}
		}
	}
  
	protected void LateUpdate()
	{
		// Refactoriar esto - controlador de animaciones
		if ( Input.GetAxisRaw("Horizontal") != 0 && _isGrounded == true && isAttacking == false && isDashing == false) {
			ChangeAnimationState("run");
		}
		if (Input.GetAxisRaw("Horizontal") == 0 && _isGrounded == true && isAttacking == false && isDashing == false) {
			ChangeAnimationState("idle");
		}
		if (Input.GetAxisRaw("Horizontal") != 0 && _isGrounded == false && isAttacking == false && isDashing == false || Input.GetAxisRaw("Horizontal") == 0 && _isGrounded == false && isAttacking == false && isDashing == false) {
			ChangeAnimationState("jump");
		}
		if (isDashing == true && isAttacking == false) {
			ChangeAnimationState("dash");
		}
		if (isAttacking == true && isDashing == false)
		{
			ChangeAnimationState("attack"); 
		}
	}
	
	private IEnumerator Dash()
	{
		canDash = false;
		isDashing = true;
		float originalGravity = _rigidbody.gravityScale;
		_rigidbody.gravityScale = 0f;
		if (_spriterender.flipX == true)
		{
			_rigidbody.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
		}
		else if (_spriterender.flipX == false)
		{
			_rigidbody.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
		}
		_trail.emitting = true;
		yield return new WaitForSeconds(dashingTime);
		_trail.emitting = false;
		_rigidbody.gravityScale = originalGravity;
		isDashing = false;
		yield return new WaitForSeconds(dashingCooldown);
		canDash = true;
	}
	
	private IEnumerator Attack()
	{
		isAttacking = true;
		if (_isGrounded == true)
		{
			_movement = Vector2.zero;
			_rigidbody.velocity = Vector2.zero;
		}
		//  Dash al atacar xd?
		if (!facingRight)
			_rigidbody.velocity = new Vector2(knockBackForce *  1.5f, _rigidbody.velocity.y);
		else
			_rigidbody.velocity = new Vector2(-knockBackForce *  1.5f, _rigidbody.velocity.y);
		
		yield return new WaitForSeconds(0.3f);
		isAttacking = false;
	}
	
	// flip the character
	private void Flip() {
		facingRight = !facingRight;
		Vector3 playerScale =  this.transform.localScale;
		playerScale.x *=  -1;
		this.transform.localScale = playerScale;
	}
	
	// knockback
	public void KnockBack()
	{
		knockBackCounter = knockBackLenght;
		_rigidbody.velocity = new Vector2(0f, knockBackForce);
		
	}
	// Animator state machine?
	private void ChangeAnimationState(string newState)
	{
		if (currentState == newState) return;
		_animator.Play(newState);
		currentState = newState;
	}
}
