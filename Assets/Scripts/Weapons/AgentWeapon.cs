using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    protected float desiredAngle;

    [SerializeField]
    protected WeaponRenderer weaponRenderer;

    [SerializeField]
    protected Weapon weapon;

    public Animator animator;

    private void Awake()
    {
        AssignWeapon();
    }

    public void AssignWeapon()
    {
        weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        weapon = GetComponentInChildren<Weapon>();
        animator = GetComponentInChildren<Animator>();
    }

    public virtual void AimWeapon(Vector2 pointerPosition)
    {
        var aimDirection = (Vector3)pointerPosition - transform.position;
        desiredAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        AdjustWeaponRendering(pointerPosition);
        transform.rotation = Quaternion.AngleAxis(desiredAngle, Vector3.forward);
    }


    public void AdjustWeaponRendering(Vector2 pointerPosition)
    {
        if (weaponRenderer != null)
        { 
            bool isPointerLeftOfPlayer = pointerPosition.x < transform.position.x;
            animator.SetBool("isFlipped", isPointerLeftOfPlayer);
            weaponRenderer.FlipSprite(isPointerLeftOfPlayer);
            bool shouldRenderBehindHead = pointerPosition.y > transform.position.y;
            weaponRenderer.RenderBehindHead(shouldRenderBehindHead);
        }
    }


    public void Attack()
    {
        weapon?.UseWeapon();
    }
}
