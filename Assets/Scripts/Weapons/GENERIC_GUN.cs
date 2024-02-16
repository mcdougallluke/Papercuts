using UnityEngine;

public class GENERIC_GUN : Weapon
{
	public Texture2D bulletTexture;
	public GameObject bulletPrefab;
	public float bulletSpeed = 8f;

	private int damage = 25;

	// Update is called once per frame
	public void Update()
    {
		if (!isEquipped)
		{
			DetectWeaponPickup();

			//Default rotation
			transform.rotation = Quaternion.Euler(0, 0, -90);
		}
		else
		{
			RotateSelf();
		}
	}

	override public void Attack()
	{
		//Calculate bullet direction
		Vector3 bulletDirectionVec = Camera.main.ScreenToWorldPoint(Input.mousePosition) - barrelTipTransform.position;

		//Create bullet
		GameObject newBullet = Instantiate(bulletPrefab, barrelTipTransform.position, Quaternion.identity);

		Bullet bulletScript = newBullet.GetComponent<Bullet>();

		// Set the texture, direction, and  speed for the bullet
		bulletScript.Initialize(damage, bulletSpeed, bulletDirectionVec.normalized, bulletTexture, bulletScript.gameObject);
		
	}
}
