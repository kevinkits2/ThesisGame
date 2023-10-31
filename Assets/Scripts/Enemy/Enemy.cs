using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private enum EnemyState {
        Idle,
        Follow,
        Attack,
    }

    [SerializeField] EnemyAnimator enemyAnimator;
    [SerializeField] EnemyIdleState enemyIdleState;
    [SerializeField] EnemyFollowState enemyFollowState;
    [SerializeField] EnemyAttackState enemyAttackState;

    private EnemyState enemyState;
    private Transform target;


    private void Awake() {
        enemyState = EnemyState.Idle;

        enemyIdleState.enabled = true;
        enemyFollowState.enabled = false;
        enemyAttackState.enabled = false;
    }

    private void Start() {
        enemyIdleState.OnTargetFound += HandleTargetFound;
        enemyFollowState.OnReadyToAttack += HandleReadyToAttack;
        enemyFollowState.OnTargetLost += HandleTargetLost;  
    }

    private void HandleTargetLost(object sender, System.EventArgs e) {
        target = null;

        ChangeState(EnemyState.Idle);
    }

    private void HandleReadyToAttack(object sender, System.EventArgs e) {
        ChangeState(EnemyState.Attack);
    }

    private void HandleTargetFound(Transform obj) {
        target = obj;
        ChangeState(EnemyState.Follow);
    }

    private void ChangeState(EnemyState state) {
        switch(state) {
            case EnemyState.Idle:
                enemyIdleState.enabled = true;
                enemyFollowState.enabled = false;
                enemyAttackState.enabled = false;

                enemyAnimator.PlayIdleAnimation();

                break;

            case EnemyState.Follow:
                enemyIdleState.enabled = false;
                enemyFollowState.enabled = true;
                enemyAttackState.enabled = false;

                enemyFollowState.SetTarget(target);
                enemyAnimator.PlayFollowAnimation();

                break;

            case EnemyState.Attack:
                enemyIdleState.enabled = false;
                enemyFollowState.enabled = false;
                enemyAttackState.enabled = true;

                break;
        }

        enemyState = state;
    }
}
