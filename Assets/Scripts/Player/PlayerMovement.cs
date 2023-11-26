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
    [SerializeField] private float playerDashingTime;
    [SerializeField] private float playerDashCooldown;

    private Rigidbody2D playerRigibody;
    private Vector2 moveVectorNormalized;
    private bool isMoving;
    private bool isDashing;
    private bool dashOnCooldown;


    private void Awake() {
        playerRigibody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        InputManager.Instance.OnDashAction += HandleDashAction;
    }

    private void HandleDashAction(object sender, System.EventArgs e) {
        if (dashOnCooldown || isDashing) return;
        if (moveVectorNormalized.sqrMagnitude <= 0) return;

        StartCoroutine(Dash());
    }

    private void Update() {
        GetMoveVector();
    }

    private void FixedUpdate() {
        if (!isDashing) {
            Move();
        }
    }

    private void Move() {
        // Old movement
        //transform.position += moveVectorNormalized * Time.deltaTime * playerMoveSpeed;

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

    private void PlayDashGhostEffect() {
        Player.Instance.PlayDashGhostEffect();
    }

    private void StopDashGhostEffect() {
        Player.Instance.StopDashGhostEffect();
    }
}
