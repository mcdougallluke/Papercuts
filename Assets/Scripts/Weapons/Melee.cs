using UnityEngine;

public class Melee : Weapon
{
	public Texture2D bulletTexture;
	public GameObject bulletPrefab;
	public float bulletSpeed = 8f;

    private int damage = 10;

    override public void Attack() 
    {
        //Calculate bullet direction
		Vector3 bulletDirectionVec = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

		//Create bullet
		GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

		Bullet bulletScript = newBullet.GetComponent<Bullet>();

		// Set the texture, direction, and  speed for the bullet
		bulletScript.Initialize(damage, bulletSpeed, RotateVector(bulletDirectionVec, 30), bulletTexture, bulletScript.gameObject);
		bulletScript.Initialize(damage, bulletSpeed, bulletDirectionVec.normalized, bulletTexture, bulletScript.gameObject);
		bulletScript.Initialize(damage, bulletSpeed, RotateVector(bulletDirectionVec, -30), bulletTexture, bulletScript.gameObject);
    }

	private Vector2 RotateVector(Vector2 toRotate, int degrees) 
	{
		toRotate = toRotate.normalized;

		Vector2 row1 = new Vector2(Mathf.Cos(degrees), -Mathf.Sin(degrees));
		Vector2 row2 = new Vector2(Mathf.Sin(degrees), Mathf.Cos(degrees));

		float newX = (row1.x * toRotate.x) + (row1.y * toRotate.y);
		float newY = (row2.x * toRotate.x) + (row2.x * toRotate.y);

		Debug.Log(newX + " " + newY);

		return new Vector2(newX, newY).normalized;
	}
}
