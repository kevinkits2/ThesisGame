using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour {

    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;


    private void Awake() {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        if (knockback.GettingKnockedBack) return;

        rb.MovePosition((Vector2)transform.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));

        if (moveDirection.x < 0) {
            spriteRenderer.flipX = true;
        }
        else if (moveDirection.x > 0) {
            spriteRenderer.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetPosition) {
        moveDirection = targetPosition;
    }

    public void StopMoving() {
        moveDirection = Vector3.zero;
    }
}
