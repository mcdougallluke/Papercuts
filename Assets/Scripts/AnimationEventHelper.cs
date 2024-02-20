using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered, OnAttackPerformed;

    [SerializeField]
    public PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
    }

    public void LockWeaponSwitching()
    {
        if (playerInput != null)
            playerInput.LockWeaponSwitching();
    }

    public void UnlockWeaponSwitching()
    {
        if (playerInput != null)
            playerInput.UnlockWeaponSwitching();
    }

    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
    }

    public void TriggerAttack()
    {
        OnAttackPerformed?.Invoke();
    }
}
