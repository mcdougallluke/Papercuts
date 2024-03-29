using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent OnShoot { get; set; }

    [field: SerializeField]
    public UnityEvent OnShootNoAmmo { get; set; }

    [field: SerializeField]
    public UnityEvent<int> OnAmmoChange { get; set; }

    public abstract void UseWeapon();
}
