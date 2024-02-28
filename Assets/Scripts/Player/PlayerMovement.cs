using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public event EventHandler<OnDashEventArgs> OnDash;
    
    public class OnDashEventArgs : EventArgs {
        public float dashCooldown;

        public OnDashEventArgs(float dashCooldown) {
            this.dashCooldown = dashCooldown;
        }
    }

    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float playerDashPower;
    [SerializeField] private float empoweredDashPower;
    [SerializeField] private float playerDashingTime;
    [SerializeField] private float playerDashCooldown;

    private PlayerSkillSO empoweredDashSkill;
    private Rigidbody2D playerRigibody;
    private Collider2D playerCollider;
    private Knockback knockback;
    private Vector2 moveVectorNormalized;
    private bool isMoving;
    private bool isDashing;
    private bool isEmpoweredDashing;
    private bool dashOnCooldown;
    private bool dashPoweredUp;
    private bool empoweredDashOnCooldown;


    private void Awake() {
        playerRigibody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        knockback = GetComponent<Knockback>();
    }

    private void Start() {
        InputManager.Instance.OnDashAction += HandleDashAction;
    }

    private void HandleDashAction(object sender, System.EventArgs e) {
        if (dashOnCooldown || isDashing) return;
        if (moveVectorNormalized.sqrMagnitude <= 0) return;
        if (knockback.GettingKnockedBack) return;

        if (empoweredDashOnCooldown || !dashPoweredUp) {
            StartCoroutine(Dash());
        }
        else {
            StartCoroutine(EmpoweredDash());
        }
    }

    private void Update() {
        GetMoveVector();
    }

    private void FixedUpdate() {
        if (!isDashing && !knockback.GettingKnockedBack) {
            Move();
        }
    }

    private void Move() {
        playerRigibody.velocity = new Vector2(moveVectorNormalized.x, moveVectorNormalized.y) * playerMoveSpeed;
    }

    private void GetMoveVector() {
        Vector2 inputVector = InputManager.Instance.GetMovementVectorNormalized();
        moveVectorNormalized = new Vector2(inputVector.x, inputVector.y);

        if (moveVectorNormalized.sqrMagnitude > 0) {
            isMoving = true;
        }
        else {
            isMoving = false;
        }
    }

    public bool IsMoving() {
        return isMoving;
    }

    public float GetDashCooldown() {
        return playerDashCooldown;
    }

    private IEnumerator Dash() {
        OnDash?.Invoke(this, new OnDashEventArgs(playerDashCooldown));

        isMoving = false;
        isDashing = true;
        playerRigibody.velocity = moveVectorNormalized * playerDashPower;
        PlayDashGhostEffect();

        yield return new WaitForSeconds(playerDashingTime);

        playerRigibody.velocity = Vector2.zero;
        StopDashGhostEffect();
        isMoving = true;
        isDashing = false;
        dashOnCooldown = true;

        yield return new WaitForSeconds(playerDashCooldown);
        dashOnCooldown = false;
    }

    private IEnumerator EmpoweredDash() {
        OnDash?.Invoke(this, new OnDashEventArgs(playerDashCooldown));

        isMoving = false;
        isDashing = true;
        isEmpoweredDashing = true;
        SetColliderToTrigger();
        playerRigibody.velocity = moveVectorNormalized * empoweredDashPower;
        PlayDashGhostEffect();

        yield return new WaitForSeconds(playerDashingTime * 2);

        playerRigibody.velocity = Vector2.zero;
        StopDashGhostEffect();
        RemoveColliderTrigger();
        isMoving = true;
        isDashing = false;
        isEmpoweredDashing = false;
        empoweredDashOnCooldown = true;

        yield return new WaitForSeconds(empoweredDashSkill.skillCooldownTime);
        empoweredDashOnCooldown = false;
    }

    private void PlayDashGhostEffect() {
        Player.Instance.PlayDashGhostEffect();
    }

    private void StopDashGhostEffect() {
        Player.Instance.StopDashGhostEffect();
    }

    private void SetColliderToTrigger() {
        playerCollider.isTrigger = true;
    }

    private void RemoveColliderTrigger() {
        playerCollider.isTrigger = false;
    }

    public void PowerUpDash(PlayerSkillSO skill) {
        dashPoweredUp = true;
        empoweredDashSkill = skill;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!isEmpoweredDashing) return;

        if (collision.gameObject.TryGetComponent<IBreakable>(out IBreakable breakable)) {
            breakable.Break();
        }
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable)) {
            damageable.TakeDamage(empoweredDashSkill.skillDamage);
        }
    }
}
