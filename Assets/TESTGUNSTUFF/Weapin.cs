using UnityEngine;
using UnityEngine.Events;

public abstract class Weapin : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent OnShoot { get; set; }

    [field: SerializeField]
    public UnityEvent OnShootNoAmmo { get; set; }

    public abstract void UseWeapon();
}
