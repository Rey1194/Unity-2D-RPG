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
	public bool facingRight;
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
	
	private float knockBackCounter;
	private float knockBackLenght = 0.25f;
	private float knockBackForce = 5f;

	// camera system
	[Header("Camera Stuff")]
	private CameraFollowObject _cameraFollowObject;
	private float _fallSpeedYDampingChangeThreshold;
	[SerializeField] private GameObject _cameraFollow;

	protected void Awake()
	{
		instance = this;
		_rigidbody = GetComponent<Rigidbody2D>();
		_trail = GetComponent<TrailRenderer>();
	}
	
	void Start()
	{
		_animator = GetComponent<Animator>();
        _cameraFollowObject = _cameraFollow.GetComponent<CameraFollowObject>();
		_fallSpeedYDampingChangeThreshold = CameraManager.instance._fallSpeedYDampingChangeThreshold;
    }

	void Update()
	{
		// if we are falling past a certaing speed threshold
		if (_rigidbody.velocity.y < _fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYdamping && !CameraManager.instance.LerpedFromPlayerFalling)
		{
			CameraManager.instance.LerpYDamping(true);
		}

		// if we are stading still or moving up
		if( _rigidbody.velocity.y >= 0f && !CameraManager.instance.IsLerpingYdamping && CameraManager.instance.LerpedFromPlayerFalling )
		{
			// reset so it can be calling again
			CameraManager.instance.LerpedFromPlayerFalling = false;

			CameraManager.instance.LerpYDamping(false);
		}
	}
	
	protected void FixedUpdate()
	{
		if (isDashing == true) {
			return;
		}

        // flip the character?
        if (isAttacking == false)
        {
            // Get the player inputs
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            _movement = new Vector2(horizontalInput, 0f);
            // flip
            if (horizontalInput > 0f && !facingRight)
            {
                Flip();
            }
            else if (horizontalInput < 0f && facingRight)
            {
                Flip();
            }
        }
        // Jump
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadio, groundLayer);
        if (Input.GetButtonDown("Jump") && _isGrounded == true)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        // Dash
        if (Input.GetKeyDown(KeyCode.LeftAlt) && canDash == true && _isGrounded == true)
        {
            StartCoroutine(Dash());
        }
        // wanna attack
        if (Input.GetButton("Fire1") && isAttacking == false)
        {
            StartCoroutine(Attack());
        }
		// Movement
        if (isAttacking == false && isDashing == false && knockBackCounter <= 0) {
			float horizontalVelocity = _movement.normalized.x * _speed;
			_rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
		}
		else if (isAttacking == false) {
			// knockback horizontal
			knockBackCounter -= Time.deltaTime;
			if (!facingRight)
			{
				_rigidbody.velocity = new Vector2(-knockBackForce, _rigidbody.velocity.y);
			}
			else
			{
				_rigidbody.velocity = new Vector2(knockBackForce, _rigidbody.velocity.y);
			}
		}
	}
  
	protected void LateUpdate()
	{
        // Controlador de animaciones (Refactorizado con switch)
        string animationState;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        bool isIdle = horizontalInput == 0 && _isGrounded && !isAttacking && !isDashing;

        // Determinar el estado de la animación
        switch (true)
        {
            case true when horizontalInput != 0 && _isGrounded && !isAttacking && !isDashing:
                animationState = "run";
                break;
            case true when isIdle:
                animationState = "idle";
                break;
            case true when (horizontalInput != 0 || horizontalInput == 0) && !_isGrounded && !isAttacking && !isDashing:
                animationState = "jump";
                break;
            case true when isDashing && !isAttacking:
                animationState = "dash";
                break;
            case true when isAttacking && !isDashing:
                animationState = "attack";
                break;
            default:
                animationState = "defaultState"; // Cambia esto según tu lógica predeterminada
                break;
        }

        ChangeAnimationState(animationState);
    }

    private IEnumerator Dash()
	{
		canDash = false;
		isDashing = true;
		float originalGravity = _rigidbody.gravityScale;
		_rigidbody.gravityScale = 0f;
        _rigidbody.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
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
		//  Dash al atacar?
		/*if (!facingRight)
			_rigidbody.velocity = new Vector2(knockBackForce *  1.5f, _rigidbody.velocity.y);
		else
        _rigidbody.velocity = new Vector2(-knockBackForce *  1.5f, _rigidbody.velocity.y);*/
		yield return new WaitForSeconds(0.3f);
		isAttacking = false;
	}

	
	// flip the character
	private void Flip() {
		if (facingRight)
		{
			Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
			transform.rotation = Quaternion.Euler(rotator);
			facingRight = !facingRight;
			// turn the camera follow object
			_cameraFollowObject.CallTurn();
		}
		else
		{
			Vector3 rotator = new Vector3(transform.rotation.x, 0, transform.rotation.z);
			transform.rotation = Quaternion.Euler(rotator);
			facingRight = !facingRight;
			// turn the camera follow object
			_cameraFollowObject.CallTurn();
        }
    }
	
	// knockback
	public void KnockBack()
	{
        // knockback vertical llamado por el Health.cs
		knockBackCounter = knockBackLenght;
		_rigidbody.velocity = new Vector2(0f, knockBackForce);
    }
    
	// Animator state machine
	private void ChangeAnimationState(string newState)
	{
		if (currentState == newState) return;
		_animator.Play(newState);
		currentState = newState;
	}
}
