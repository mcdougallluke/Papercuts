using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float health, maxHealth = 100f;
	[SerializeField] HealthBar healthBar;

	public Weapon[] equippedWeapons = new Weapon[2];
	public Weapon weaponInHand = null;
	public int currWeaponIndex = 0;

    public Rigidbody2D rb;
	public float moveSpeed;
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
		healthBar = GetComponentInChildren<HealthBar>();

		try
		{
			//Try to initialize starter weapon
			equippedWeapons[0].transform.SetParent(transform, false);
			equippedWeapons[0].transform.position = transform.position;
			weaponInHand = equippedWeapons[0];
		} catch (System.Exception)
		{
			//Occurs when no weapon in hand
		} 
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

	public void TakeDamage(float damage)
	{
		health -= damage;

		healthBar.UpdateHealthBar(health, maxHealth);

		//TODO: Game over screen
		if ( health <= 0 ) {}
	}
	private void PerformAttack(InputAction.CallbackContext obj)
	{
		animator.SetTrigger("Attack");
	}

    private void DropWeapon()
    {
		//Check if weapon equipped
		if (!weaponInHand.IsUnityNull())
		{
			//Detach weapon, sort behind player, set default rotation
			weaponInHand.transform.SetParent(null);
			weaponInHand.GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder - 1;
			weaponInHand.transform.rotation = Quaternion.Euler(0, 0, -90);
			weaponInHand.isEquipped = false;
			weaponInHand = null;
		}
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
			DropWeapon();
        }

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (weaponInHand != null)
			{
				weaponInHand.Attack();
			}
		}
		
		//Change weapon
		if (Input.GetKeyDown(KeyCode.F))
		{
			//Deactive weapon in hand
			if (weaponInHand != null)
			{
				weaponInHand.myGameObject.SetActive(false);
			}

			//Swap weapon in hand
			currWeaponIndex = (currWeaponIndex == 0 ? 1 : 0);
			weaponInHand = equippedWeapons[currWeaponIndex];

			//activate new weapon
			if (weaponInHand != null)
			{
				weaponInHand.myGameObject.SetActive(true);
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
