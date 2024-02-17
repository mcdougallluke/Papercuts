using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeleeWeapon : MonoBehaviour
{
    public Animator animator;
    public float attackDelay = 0.3f;
    private bool isAttackBlocked;

    public void Attack()
    {
        if (isAttackBlocked) return;

        animator.SetTrigger("Attack");
        isAttackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttackBlocked = false;
    }

    public void PerformAttack()
    {
        // Implement collision detection logic here, similar to DetectColliders method
    }
}
