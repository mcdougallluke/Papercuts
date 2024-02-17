using System.Collections;
using UnityEngine;

public class GunWeapon : Weapon
{
    [SerializeField]
    private GameObject muzzle;

    [SerializeField]
    private int ammo;

    [SerializeField]
    private WeaponDataSO weaponData;

    private bool isShooting = false;
    private bool reloadCoroutine = false;

    public int Ammo
    {
        get { return ammo; }
        set { ammo = Mathf.Clamp(value, 0, weaponData.AmmoCapacity); }
    }

    protected void Start()
    {
        Ammo = weaponData.AmmoCapacity;
    }

    public override void UseWeapon()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    public void Reload(int ammoToAdd)
    {
        Ammo += ammoToAdd;
    }

    private void Update()
    {
        FireWeapon();
    }

    public void FireWeapon()
    {
        if (isShooting && reloadCoroutine == false)
        {
            if (Ammo > 0)
            {
                Ammo--;
                OnShoot?.Invoke();
                for (int i = 0; i < weaponData.GetBulletCountToSpawn(); i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                isShooting = false;
                OnShootNoAmmo?.Invoke();
                return;
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
