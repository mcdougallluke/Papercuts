using UnityEngine;
using UnityEngine.Events;

public class AutoProximityPickUp : MonoBehaviour
{
	public float pickUpRadius = 1.75f;

	public LayerMask whatIsPaper;
	public LayerMask whatIsHealth;
	public LayerMask whatIsAmmo;

	[SerializeField]
	private Health playerHealth;
	[SerializeField]
	private GunWeapon playerGun;
	[SerializeField]
	private UIAmmo ammoUI;

	public int paperCount { get; private set; } = 0;

	[field: SerializeField]
	public UnityEvent OnPickup { get; set; }

	void Update()
	{
		// Automatically pick up the closest paper within pick-up radius
		Collider2D[] collidedPapers = Physics2D.OverlapCircleAll(transform.position, pickUpRadius, whatIsPaper);
		Collider2D[] collidedHealth = Physics2D.OverlapCircleAll(transform.position, pickUpRadius - 0.25f, whatIsHealth);
		Collider2D[] collidedAmmo = Physics2D.OverlapCircleAll(transform.position, pickUpRadius - 0.25f, whatIsAmmo);

		if (collidedPapers.Length > 0)
		{
			foreach (var collider in collidedPapers)
			{
				PickUpPaper(collider.gameObject); // Pick up each paper in range
			}
		}

		if (collidedHealth.Length > 0)
		{
			foreach (var collider in collidedHealth)
			{
				PickUpHealth(collider.gameObject);
			}
		}

		if (collidedAmmo.Length > 0)
		{
			foreach (var collider in collidedAmmo)
			{
				PickUpAmmo(collider.gameObject);
			}
		}
	}

	void PickUpAmmo(GameObject ammo)
	{
		if (ammo != null && ammo.CompareTag("Ammo"))
		{
			if (playerGun.AmmoFull) return;

			ammo.SetActive(false);
			playerGun.ammo += 5;
			ammoUI?.UpdateBulletsText(playerGun.ammo);
			OnPickup?.Invoke();
		}
	}

	void PickUpHealth(GameObject health)
	{
		if (health != null && health.CompareTag("Health"))
		{
			if (playerHealth.currentHealth == playerHealth.maxHealth) return;
			health.SetActive(false);
			playerHealth?.GetHit(-25, health); //Give player more health
			OnPickup?.Invoke();
		}
	}

	void PickUpPaper(GameObject Paper)
	{
		if (Paper != null && Paper.CompareTag("Paper"))
		{
			Paper.SetActive(false); // Remove paper object from scene
			paperCount++; // Add paper to inventory
			OnPickup?.Invoke();
			Debug.Log("Picked up a paper. Total papers: " + paperCount); //Print to console
		}
	}

	private void OnDrawGizmosSelected()
	{
		//visualize pick-up radius in the editor
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, pickUpRadius);
	}
}
