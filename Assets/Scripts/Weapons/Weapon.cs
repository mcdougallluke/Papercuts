using UnityEngine;


public class Weapon : MonoBehaviour
{
	public GameObject myGameObject = null;
	public PlayerController player;
	public Transform barrelTipTransform;
	public int ammo = 25;
	public bool isEquipped = false;

	/*
	 * CHILDREN MUST IMPLEMENT METHOD FOR ATTACK TYPE
	 */
	virtual public void Attack() {}

	public void Awake() 
	{
		myGameObject = gameObject;
		
		barrelTipTransform = GetComponentInChildren<BulletTipMarker>().transform;

		//if (barrelTipTransform == null) barrelTipTransform = transform;
	}


	protected void DetectWeaponPickup()
	{	
		if (Vector3.Distance(transform.position, player.transform.position) > 2) return;
		
		//Detect if player picking up weapon
		if (Input.GetKeyDown(KeyCode.E))
		{
			transform.SetParent(player.transform, false);
			transform.position = player.transform.position;
			GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + 1;


			//Check if player holding wepaon
			if (player.weaponInHand == null)
			{
				player.weaponInHand = this;
				player.equippedWeapons[player.currWeaponIndex] = this;
			} else
			{
				//Deactivate and put this weapon in opposite slot
				myGameObject.SetActive(false);
				player.equippedWeapons[player.currWeaponIndex == 0 ? 1 : 0] = this;
			}

			isEquipped = true;
		}
	}

	protected void RotateSelf()
	{
		//Rotate sprite to point at cursor
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 direction = worldPos - barrelTipTransform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
}