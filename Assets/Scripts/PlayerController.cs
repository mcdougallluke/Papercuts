using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Weapon equippedRangedWeapon = null;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection = Vector2.down;
    private bool isMoving;
    private Animator animator;

	[SerializeField]
	private InputActionReference movement, attack;

	private Vector2 movementInput;


	private void Awake()
    {
        animator = GetComponent<Animator>();

        equippedRangedWeapon.transform.SetParent(transform, false);
        equippedRangedWeapon.transform.position = transform.position;
    }

	private void OnEnable()
	{
		attack.action.performed += PerformAttack;
	}

	private void OnDisable()
	{
		attack.action.performed -= PerformAttack;
	}

	public void Update()
	{
		ProcessInputs();
		Animate();
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void PerformAttack(InputAction.CallbackContext obj)
	{
		animator.SetTrigger("Attack");
	}

	void ProcessInputs()
    {
		movementInput = movement.action.ReadValue<Vector2>();

		moveDirection = movementInput.normalized;

		if (moveDirection != Vector2.zero)
		{
			lastMoveDirection = moveDirection;
		}

		//Drop weapon
		if (Input.GetKeyDown(KeyCode.Q)) 
        {
            if (!equippedRangedWeapon.IsUnityNull())
            {
                equippedRangedWeapon.transform.SetParent(null);
                equippedRangedWeapon.isEquipped = false;
                equippedRangedWeapon = null;
            }
        }
    }

    void Move()
    {
        if (moveDirection != Vector2.zero) // Check if the character is moving
        {
            isMoving = true;
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }
        else
        {
            isMoving = false;
            rb.velocity = Vector2.zero; // Stop the character
        }
    }

    void Animate()
    {
        animator.SetFloat("moveX", lastMoveDirection.x);
        animator.SetFloat("moveY", lastMoveDirection.y);
        animator.SetBool("isMoving", isMoving);
    }
}
