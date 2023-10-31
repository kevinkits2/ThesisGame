using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : MonoBehaviour {

    private const float PLAYER_COLLIDER_COMPENSATION = 0.5f;

    public event EventHandler OnReadyToAttack;
    public event EventHandler OnTargetLost;

    [SerializeField] private float enemyMoveSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private CircleCollider2D aggroCollider;

    private Transform target;
    private float aggroRange;


    private void Awake() {
        aggroRange = aggroCollider.radius;
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

    public void Update() {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        transform.position += direction * Time.deltaTime * enemyMoveSpeed;

        if (Vector3.Distance(target.position, transform.position) <= attackRange) {
            OnReadyToAttack.Invoke(this, EventArgs.Empty);
        }

        else if (Vector3.Distance(target.position, transform.position) > aggroRange + PLAYER_COLLIDER_COMPENSATION) {
            target = null;

            OnTargetLost.Invoke(this, EventArgs.Empty);
        }
    }
}
