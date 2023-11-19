using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyProjectileAttack : MonoBehaviour {

    private EnemyProjectileAttackSO enemyProjectileAttackSO;
    private EnemyTargetScanner enemyTargetScanner;
    private Transform target;
    private Vector2[] projectileDirections;
    private bool readyToAttack;


    private void Start() {
        projectileDirections = new Vector2[enemyProjectileAttackSO.projectileAmount];

        enemyTargetScanner = GetComponent<EnemyTargetScanner>();

        enemyTargetScanner.OnTargetFound += HandleTargetFound;
        enemyTargetScanner.OnTargetLost += HandleTargetLost;
    }

    private void Update() {
        if (!readyToAttack || target == null) return;

        CalculateProjectileDirections();
        ShootProjectiles();
    }

    private void HandleTargetLost() {
        target = null;
        readyToAttack = false;
    }

    private void HandleTargetFound(Transform obj) {
        target = obj;
        readyToAttack = true;
    }

    public void ShootProjectiles() {
        for (int i = 0; i < projectileDirections.Length; i++) {
            GameObject instantiatedProjectile = Instantiate(enemyProjectileAttackSO.projectilePrefab,
               transform.position, Quaternion.identity);

            if (!instantiatedProjectile.TryGetComponent<EnemyProjectile>(out EnemyProjectile projectile)) return;

            projectile.Init(projectileDirections[i], enemyProjectileAttackSO.projectileSpeed, enemyProjectileAttackSO.projectileDamage,
                enemyProjectileAttackSO.projectileLifetime);
        }

        StartCoroutine(AttackCooldown());
    }

    private void CalculateProjectileDirections() {
        float projectileAmount = enemyProjectileAttackSO.projectileAmount;
        Vector3 shootDirection = target.position - transform.position;

        for (int i = 0; i < projectileAmount; i++) {
            float spreadAngle = enemyProjectileAttackSO.projectileSpreadStartAngle + i * enemyProjectileAttackSO.projectileSpread;
            Vector3 projectileDirection = Quaternion.AngleAxis(spreadAngle / 2, Vector3.forward) * shootDirection;
            
            projectileDirections[i] = new Vector2(projectileDirection.x, projectileDirection.y).normalized;
        }
    }

    private IEnumerator AttackCooldown() {
        float timer = 0f;
        readyToAttack = false;

        while (timer < enemyProjectileAttackSO.projectileAttackCooldown) {
            timer += Time.deltaTime;

            yield return null;
        }

        readyToAttack = true;
    }

    public void SetProjectileAttack(EnemyProjectileAttackSO projectileAttack) {
        enemyProjectileAttackSO = projectileAttack;
    }
}
