using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public PlayerController player;
	public Texture2D bulletTexture;
    public GameObject bulletPrefab;
	public float bulletSpeed = 8f;
	public bool isEquipped = false;

	// Update is called once per frame
	void Update()
	{
		if (!isEquipped)
		{
			DetectWeaponPickup();
		}
		else
		{

			RotateSelf();
			ProcessInput();
		}
	}

	void DetectWeaponPickup()
	{
		//Stop detection if weapon already equipped
		if (player.equippedRangedWeapon != null) return;
		
		if (Vector3.Distance(this.transform.position, player.transform.position) > 2) return;

		if (Input.GetKeyDown(KeyCode.E))
		{
			this.transform.SetParent(player.transform, false);
			this.transform.position = player.transform.position;
			player.equippedRangedWeapon = this;
			isEquipped = true;
		}
		
	}

	void RotateSelf()
	{
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// Calculate the direction from the sprite to the mouse cursor
		Vector3 direction = worldPos - transform.position;
		// Calculate the angle in degrees
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		// Rotate the sprite to face the mouse cursor
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
	}

	void ProcessInput()
	{

		//Fire Weapon if left click
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			//Calculate bullet direction
			Vector3 bulletDirectionVec = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

			//Create bullet
			GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

			Bullet bulletScript = newBullet.GetComponent<Bullet>();

			// Set the texture, direction, and  speed for the bullet
			bulletScript.Initialize(bulletSpeed, bulletDirectionVec.normalized, bulletTexture, bulletScript);
		}
	}
}