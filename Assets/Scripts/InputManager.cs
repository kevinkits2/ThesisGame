using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager Instance { get; private set; }

    public event EventHandler OnDashAction;
    public event EventHandler OnAttackAction;
    public event EventHandler OnSkillTreeAction;
    public event EventHandler OnStatsAction;
    public event EventHandler OnFallingSwordsAction;

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

        playerControls.UI.SkillTree.performed += HandleSkillTreePerfromed;
        playerControls.UI.Stats.performed += HandleStatsPerformed;

        playerControls.Skill.FallingSwords.performed += HandleFallingSwordsPerformed;
    }

    private void HandleFallingSwordsPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnFallingSwordsAction?.Invoke(this, EventArgs.Empty);
    }

    private void HandleStatsPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnStatsAction?.Invoke(this, EventArgs.Empty);
    }

    private void HandleSkillTreePerfromed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnSkillTreeAction?.Invoke(this, EventArgs.Empty);
    }

    private void HandleDashPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnDashAction?.Invoke(this, EventArgs.Empty);
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

    public void DisablePlayerControls() {
        playerControls.Player.Disable();
    }

    public void EnablePlayerControls() {
        playerControls.Player.Enable();
    }
}
