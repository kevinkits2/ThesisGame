using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    private enum State {
        Roaming,
        Attacking
    }

    [SerializeField] private float attackRange = 0f;
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;
    [SerializeField] private bool disableRoaming = false;

    private bool canAttack = true;

    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private State state;
    private EnemyPathfinding enemyPathfinding;


    private void Awake() {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start() {
        roamPosition = GetRoamingPosition();
    }

    private void Update() {
        MovementStateControl();
    }

    private void MovementStateControl() {
        switch (state) {
            case State.Roaming:
                Roaming();
                break;
            case State.Attacking:
                Attacking();
                break;
            default:
                break;
        }
    }

    private void Roaming() {
        timeRoaming += Time.deltaTime;

        if (!disableRoaming) {
            enemyPathfinding.MoveTo(roamPosition);
        }

        if (Vector2.Distance(transform.position, Player.Instance.transform.position) < attackRange) {
            state = State.Attacking;
        }

        if (timeRoaming > roamChangeDirFloat) {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking() {
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) > attackRange) {
            state = State.Roaming;
        }

        if (attackRange != 0 && !canAttack) return;

        canAttack = false;
        (enemyType as IEnemy).Attack();

        if (!disableRoaming) {
            if (stopMovingWhileAttacking) {
                enemyPathfinding.StopMoving();
            }
            else {
                enemyPathfinding.MoveTo(roamPosition);
            }
        }

        StartCoroutine(AttackCooldownRoutine());
    }

    private IEnumerator AttackCooldownRoutine() {
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    private Vector2 GetRoamingPosition() {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
