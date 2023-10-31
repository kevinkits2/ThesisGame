using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager Instance { get; private set; }

    public event EventHandler OnDashAction;
    public event EventHandler OnAttackAction;
    private bool attackActionHeldDown;

    private PlayerControls playerControls;


    private void Awake() {
        if (Instance != null) {
            return;
        }

        Instance = this;

        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Player.Attack.performed += HandleAttackPerformed;
        playerControls.Player.Attack.canceled += HandleAttackCanceled;

        playerControls.Player.Dash.performed += HandleDashPerformed;
    }

    private void HandleDashPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnDashAction.Invoke(this, EventArgs.Empty);
    }

    private void HandleAttackCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        attackActionHeldDown = false;
    }

    private void HandleAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
        attackActionHeldDown = true;
    }

    private void Update() {
        CheckAttackActionHeldDown();
    }

    private void CheckAttackActionHeldDown() {
        if (!attackActionHeldDown) return;

        OnAttackAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVectorNormalized = playerControls.Player.Move.ReadValue<Vector2>();

        return inputVectorNormalized;
    }

    public Vector3 GetMouseWorldPosition() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        return mousePosition;
    }
}
