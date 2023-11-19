using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowTarget : MonoBehaviour {

    private Enemy enemy;
    private EnemyTargetScanner enemyTargetScanner;
    private Transform target;
    private Rigidbody2D enemyRigidbody;


    private void Awake() {
        enemy = GetComponent<Enemy>();
        enemyTargetScanner = GetComponent<EnemyTargetScanner>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        enemyTargetScanner.OnTargetFound += HandleTargetFound;
        enemyTargetScanner.OnTargetLost += HandleTargetLost;
    }

    private void HandleTargetLost() {
        target = null;

        enemyRigidbody.velocity = Vector3.zero;
    }

    private void HandleTargetFound(Transform obj) {
        target = obj;
    }

    private void FixedUpdate() {
        if (target == null) return;

        FollowTarget();
    }

    public void FollowTarget() {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        enemyRigidbody.velocity = new Vector2(direction.x, direction.y) * enemy.GetMoveSpeed() * Time.deltaTime;
    }
}
