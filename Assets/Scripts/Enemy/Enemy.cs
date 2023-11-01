using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private enum EnemyState {
        Idle,
        Follow,
        Attack,
        Aggressive,
    }

    [SerializeField] EnemyAnimator enemyAnimator;

    private EnemyHealth enemyHealth;
    private EnemyIdleState enemyIdleState;
    private EnemyFollowState enemyFollowState;
    private EnemyAttackState enemyAttackState;
    private EnemyAggressiveState enemyAggressiveState;

    private EnemyState currentState;
    private EnemyState previousState;
    private Transform target;


    private void Awake() {
        currentState = EnemyState.Idle;

        enemyHealth = GetComponent<EnemyHealth>();
        enemyIdleState = GetComponent<EnemyIdleState>();
        enemyFollowState = GetComponent<EnemyFollowState>();
        enemyAttackState = GetComponent<EnemyAttackState>();
        enemyAggressiveState = GetComponent<EnemyAggressiveState>();

        EnableState(currentState);
    }

    private void Start() {
        enemyHealth.OnTakeDamage += HandleTakeDamage;
        enemyIdleState.OnTargetFound += HandleTargetFound;
        enemyFollowState.OnReadyToAttack += HandleFollowReadyToAttack;
        enemyFollowState.OnTargetLost += HandleFollowTargetLost;
        enemyAttackState.OnTargetLost += HandleAttackTargetLost;
        enemyAggressiveState.OnReadyToAttack += HandleAggressiveReadyToAttack;
    }

    private void HandleAggressiveReadyToAttack(object sender, System.EventArgs e) {
        ChangeState(EnemyState.Attack);
    }

    private void HandleTakeDamage(object sender, System.EventArgs e) {
        ChangeState(EnemyState.Aggressive);
    }

    private void HandleAttackTargetLost(object sender, System.EventArgs e) {
        if (previousState == EnemyState.Aggressive) {
            ChangeState(EnemyState.Aggressive);
        }
        else {
            ChangeState(EnemyState.Follow);
        }
    }

    private void HandleFollowTargetLost(object sender, System.EventArgs e) {
        target = null;

        ChangeState(EnemyState.Idle);
    }

    private void HandleFollowReadyToAttack(object sender, System.EventArgs e) {
        ChangeState(EnemyState.Attack);
    }

    private void HandleTargetFound(Transform obj) {
        target = obj;
        ChangeState(EnemyState.Follow);
    }

    private void ChangeState(EnemyState state) {
        EnableState(state);
        previousState = currentState;
        currentState = state;

        switch (state) {
            case EnemyState.Idle:
                HandleIdleStateEnabled();
                break;

            case EnemyState.Follow:
                HandleFollowStateEnabled();
                break;

            case EnemyState.Attack:
                HandleAttackStateEnabled();
                break;

            case EnemyState.Aggressive:
                HandleAggressiveStateEnabled();
                break;
        }

        currentState = state;
    }

    private void EnableState(EnemyState state) {
        enemyIdleState.enabled = EnemyState.Idle == state;
        enemyFollowState.enabled = EnemyState.Follow == state;
        enemyAttackState.enabled = EnemyState.Attack == state;
        enemyAggressiveState.enabled = EnemyState.Aggressive == state;
    }

    private void HandleIdleStateEnabled() {
        enemyAnimator.PlayIdleAnimation();
    }

    private void HandleFollowStateEnabled() {
        enemyFollowState.SetTarget(target);
        enemyAnimator.PlayFollowAnimation();
    }

    private void HandleAttackStateEnabled() {
        enemyAttackState.SetTarget(target);
    }

    private void HandleAggressiveStateEnabled() {
        enemyAggressiveState.SetTarget(Player.Instance.gameObject.transform);
    }
}
