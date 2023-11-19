using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggressiveState : MonoBehaviour {

    /*public event EventHandler OnReadyToAttack;

    [SerializeField] private ParticleSystem circleShotParticleSystem;
    [SerializeField] private float enemyMoveSpeed;
    [SerializeField] private float attackRange;

    private Rigidbody2D enemyRigidbody;
    private Transform target;
    private EnemyShotgunAttack enemyShotgunAttackComponent;


    private void Awake() {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyShotgunAttackComponent = GetComponent<EnemyShotgunAttack>();
    }


    private void Update() {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        // Old movement
        //transform.position += direction * Time.deltaTime * enemyMoveSpeed;
        enemyRigidbody.velocity = new Vector2(direction.x, direction.y) * enemyMoveSpeed * Time.deltaTime;

        if (Vector3.Distance(target.position, transform.position) <= attackRange) {
            OnReadyToAttack.Invoke(this, EventArgs.Empty);
        }
    }

    public void SetTarget(Transform target) {
        this.target = target;

        enemyShotgunAttackComponent.enabled = true;
        enemyShotgunAttackComponent.Init(target);
        circleShotParticleSystem.Play();
    }*/
}
