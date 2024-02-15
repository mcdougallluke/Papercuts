using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour
{
	[SerializeField] int currHealth = 100;
	[SerializeField] int maxHealth = 100;
	[SerializeField] int currStamina = 100;
	[SerializeField] int maxStamina = 100;
	[SerializeField] StatusBar healthBar;
	[SerializeField] StatusBar staminaBar;

	public Weapon[] equippedWeapons = new Weapon[2];
	public Weapon weaponInHand = null;
	public int currWeaponIndex = 0;

    public Rigidbody2D rb;
	
	public float moveSpeed;
	private float baseSpeed;
	private Boolean decreaseStamina = false;
	private Boolean regenStamina = false;

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

		//Initiliaze status bars
		StatusBar[] statusBars = GetComponentsInChildren<StatusBar>();
		healthBar = (statusBars[0].type == StatusBar.StatusBarType.HEALTH) ? statusBars[0] : statusBars[1];
		staminaBar = (statusBars[0].type == StatusBar.StatusBarType.STAMINA) ? statusBars[0] : statusBars[1];
		healthBar.UpdateStatusBar(currHealth, maxHealth);
		staminaBar.UpdateStatusBar(currStamina, maxStamina);

		baseSpeed = moveSpeed;
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
		if (!regenStamina & !decreaseStamina & (currStamina < maxStamina))
		{
			regenStamina = true;
			InvokeRepeating("RegenStamina", 0f, 1f);
		}

		Move();
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
			equippedWeapons[currWeaponIndex] = null;
			weaponInHand.isEquipped = false;
			weaponInHand = null;
		}
	}

	/*
	* Status Bar methods
	*/
	public void TakeDamage(int damage)
	{
		currHealth -= damage;

		healthBar.UpdateStatusBar(currHealth, maxHealth);

		//TODO: Game over screen
		if ( currHealth <= 0 ) {}
	}

	private void DecreaseStamina()
	{
        currStamina = Mathf.Clamp((currStamina - 20), 0, maxStamina);

        staminaBar.UpdateStatusBar(currStamina, maxStamina);


		if (currStamina <= 0 )
		{
			StopStaminaDecrease();
		}
	}

	private void RegenStamina()
	{
		currStamina = Mathf.Clamp((currStamina + 20), 0, maxStamina);
		staminaBar.UpdateStatusBar(currStamina, maxStamina);

		if (currStamina == 100 || decreaseStamina)
		{
			StopStaminaRegen();
		}
	}

	private void StopStaminaDecrease()
	{
        decreaseStamina = false;
		CancelInvoke("DecreaseStamina");
		moveSpeed = baseSpeed;
	}

	private void StopStaminaRegen()
	{
		CancelInvoke("RegenStamina");
		regenStamina = false;
	}


	/*
	Input processing
	*/
	void ProcessInputs()
    {
		movementInput = movement.action.ReadValue<Vector2>();

		moveDirection = movementInput.normalized;

		if (moveDirection != Vector2.zero)
		{
			lastMoveDirection = moveDirection;
		}

		//Sprint toggle
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if (currStamina > 0)
			{
                decreaseStamina = true;
                InvokeRepeating("DecreaseStamina", 0f, 0.45f);
                moveSpeed = baseSpeed * 1.5f;
			}
		}

		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			StopStaminaDecrease();
		}

		//Drop weapon
		if (Input.GetKeyDown(KeyCode.Q)) 
        {
			DropWeapon();
        }

		//Attack
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
