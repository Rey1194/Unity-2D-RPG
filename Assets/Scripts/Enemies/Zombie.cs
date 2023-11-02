using UnityEngine;

public class Zombie : MonoBehaviour
{
	public Rigidbody2D _enemyrigidbody;
	private bool _isGrounded;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private float groundCheckRadio = 0.025f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float randomValue;
	[SerializeField] private float selectedValue;
    
    // Awake is called when the script instance is being loaded.
    protected void Awake() {
        // Genera un número aleatorio (0 o 1)
        randomValue = Random.Range(0, 2);
        // Asigna el valor basado en el número aleatorio
        selectedValue = (randomValue == 0) ? 2 : -2;
    }
    
    // Start is called before the first frame update
    void start()
    {
	    _enemyrigidbody = this.GetComponent<Rigidbody2D>();

    }
    
    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    protected void FixedUpdate() 
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadio, groundLayer);
        
        if (_isGrounded)
        {
            _enemyrigidbody.velocity = (selectedValue == 2) ? new Vector2(moveSpeed, 0f) : new Vector2(-moveSpeed, 0f);
        }
    }
    
}
