using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Weapon equippedRangedWeapon = null;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection = Vector2.down;
    private bool isMoving;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        equippedRangedWeapon.transform.SetParent(transform, false);
        equippedRangedWeapon.transform.position = transform.position;
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

    void ProcessInputs()
    {
        //Movement inputs
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveDirection != Vector2.zero) // Check if there is movement
        {
            lastMoveDirection = moveDirection; // Update the last move direction
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
