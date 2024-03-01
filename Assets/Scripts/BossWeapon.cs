using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class BossWeapon : WeaponParent
{
    [SerializeField]
    private GameObject muzzle;

	[SerializeField]
	public int ammo;

	[SerializeField]
    private WeaponDataSO weaponData;

    public GameObject BulletPrefab;

	public bool AmmoFull { get => Ammo >= weaponData.AmmoCapacity; }

	private bool isShooting = false;
    private bool reloadCoroutine = false;

	public int Ammo
    {
        get { return ammo; }
        set
        {
            ammo = Mathf.Clamp(value, 0, weaponData.AmmoCapacity);
        }
    }


	public void UseWeapon()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }

	private void Update()
	{
        FireWeapon();
		if (IsAttacking)
			return;
		Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
		transform.right = direction;

		Vector2 scale = transform.localScale;
		if (direction.x < 0)
		{
			scale.y = -1;
		}
		else if (direction.x > 0)
		{
			scale.y = 1;
		}
		transform.localScale = scale;

		if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
		{
			weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
		}
		else
		{
			weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
		}
	}

    override
    public void Attack()
    {
        UseWeapon();
    }

    public void FireWeapon()
    {
        if (isShooting && reloadCoroutine == false)
        {
            //OnShoot?.Invoke();
            for (int i = 0; i < weaponData.GetBulletCountToSpawn(); i++)
            {
                ShootBullet();
            }

            FinishShooting();
        }
    }

    private void FinishShooting()
    {
        StartCoroutine(DelayNextShotCoroutine());
        if (weaponData.AutomaticFire == false)
        {
            isShooting = false;
        }
    }

    protected IEnumerator DelayNextShotCoroutine()
    {
        reloadCoroutine = true;
        yield return new WaitForSeconds(weaponData.WeaponDelay);
        reloadCoroutine = false;
    }

    private void ShootBullet()
    {
        SpawnBullet(muzzle.transform.position, CalculateAngle(muzzle));
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
		var bulletPrefab = Instantiate(weaponData.BulletData.bulletPrefab, position, rotation);
		bulletPrefab.GetComponent<Bulet>().BulletData = weaponData.BulletData;
    }

    private Quaternion CalculateAngle(GameObject muzzle)
    {
        float spread = Random.Range(-weaponData.SpreadAngle, weaponData.SpreadAngle);
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0, 0, spread));
        return muzzle.transform.rotation * bulletSpreadRotation;
    }
}
