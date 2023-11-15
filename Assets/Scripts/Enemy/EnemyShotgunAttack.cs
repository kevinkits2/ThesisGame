using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShotgunAttack : MonoBehaviour {

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float attackCooldownTime;
    [SerializeField] private float angleBetweenShots;

    private Transform target;
    private float attackTimer;
    private bool attackOnCooldown;

    public void Init(Transform target) {
        this.target = target;
        attackTimer = attackCooldownTime;
    }

    private void Update() {
        if (target == null) return;

        if (attackTimer >= attackCooldownTime) {
            Attack();
            attackTimer = 0f;
        }

        attackTimer += Time.deltaTime;
    }

    private void Attack() {
        if (attackOnCooldown) return;

        Vector3 shootDirectionMid = target.position - transform.position;
        shootDirectionMid.Normalize();

        // Calculate new vectors for angled attacks
        Vector3 shootDirectionTop = Quaternion.AngleAxis(-angleBetweenShots / 2, Vector3.forward) * shootDirectionMid;
        Vector3 shootDirectionBottom = Quaternion.AngleAxis(angleBetweenShots / 2, Vector3.forward) * shootDirectionMid;

        GameObject instantiatedProjectileTop = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        if (!instantiatedProjectileTop.TryGetComponent<EnemyProjectile>(out EnemyProjectile projectileTop)) return;

        GameObject instantiatedProjectileMid = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        if (!instantiatedProjectileMid.TryGetComponent<EnemyProjectile>(out EnemyProjectile projectileMid)) return;

        GameObject instantiatedProjectileBottom = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        if (!instantiatedProjectileBottom.TryGetComponent<EnemyProjectile>(out EnemyProjectile projectileBottom)) return;

        projectileTop.Init(shootDirectionTop);
        projectileMid.Init(shootDirectionMid);
        projectileBottom.Init(shootDirectionBottom);


        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown() {
        attackOnCooldown = true;

        yield return new WaitForSeconds(attackCooldownTime);

        attackOnCooldown = false;
    }
}
