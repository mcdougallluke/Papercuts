using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack1, OnAttack2;

    [SerializeField]
    private InputActionReference movement, attack1, attack2, pointerPosition; 

    [SerializeField]
    private GameObject swordObject, stapleGunObject;

    public bool canSwitchWeapon = true;

    private void Awake()
    {
        swordObject.SetActive(true);
        stapleGunObject.SetActive(false);
    }

    private void Update()
    {
        Vector2 input = movement.action.ReadValue<Vector2>().normalized;
        OnMovementInput?.Invoke(input);
        OnPointerInput?.Invoke(GetPointerInput());
    }

    public void LockWeaponSwitching()
    {
        canSwitchWeapon = false;
    }

    public void UnlockWeaponSwitching()
    {
        canSwitchWeapon = true;
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnEnable()
    {
       
        attack1.action.performed += PerformAttack1;
        attack2.action.performed += PerformAttack2;
    }

    private void OnDisable()
    {
        
        attack1.action.performed -= PerformAttack1;
        attack2.action.performed -= PerformAttack2;
    }

    private void PerformAttack1(InputAction.CallbackContext obj)
    {
        if (!canSwitchWeapon) return;
        swordObject.SetActive(true);
        stapleGunObject.SetActive(false);
        OnAttack1?.Invoke();
    }

    private void PerformAttack2(InputAction.CallbackContext obj)
    {
        if (!canSwitchWeapon) return;
        stapleGunObject.SetActive(true);
        swordObject.SetActive(false);
        OnAttack2?.Invoke();
    }
}
