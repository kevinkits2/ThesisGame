using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggressiveState : MonoBehaviour {

    public event EventHandler OnReadyToAttack;

    [SerializeField] private float enemyMoveSpeed;
    [SerializeField] private float attackRange;

    private Transform target;


    private void Update() {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        transform.position += direction * Time.deltaTime * enemyMoveSpeed;

        if (Vector3.Distance(target.position, transform.position) <= attackRange) {
            OnReadyToAttack.Invoke(this, EventArgs.Empty);
        }
    }

    public void SetTarget(Transform target) {
        this.target = target; 
    }
}
