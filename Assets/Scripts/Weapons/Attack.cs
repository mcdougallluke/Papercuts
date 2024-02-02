using Unity.Mathematics;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject weapon;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {

		Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calculate the direction from the sprite to the mouse cursor
        Vector3 direction = worldPos - weapon.transform.position;
		// Calculate the angle in degrees
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		// Rotate the sprite to face the mouse cursor
		weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));

        

		if (Input.GetKeyDown(KeyCode.LeftShift))
        {
			//Calculate bullet direction
            Vector3 bulletDirectionVec = Camera.main.ScreenToWorldPoint(Input.mousePosition) - weapon.transform.position;

            //Create bullet
			GameObject newBullet = Instantiate(bulletPrefab, weapon.transform.position, Quaternion.identity);

			Bullet bulletScript = newBullet.GetComponent<Bullet>();

			// Set the direction and speed for the bullet
			bulletScript.direction = bulletDirectionVec.normalized;
			bulletScript.speed = 6f;
		}
	}
}