using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyParticleAttack : MonoBehaviour {

    private EnemyParticleAttackSO enemyParticleAttack;
    private ParticleSystem particleAttack;
    private EnemyTargetScanner enemyTargetScanner;
    private Transform target;
    private bool readyToAttack;


    private void Start() {
        enemyTargetScanner = GetComponent<EnemyTargetScanner>();

        enemyTargetScanner.OnTargetFound += HandleTargetFound;
        enemyTargetScanner.OnTargetLost += HandleTargetLost;
    }

    private void HandleTargetLost() {
        target = null;
    }

    private void HandleTargetFound(Transform obj) {
        target = obj;
        readyToAttack = true;
    }

    private void Update() {
        if (target == null) return;

        if (readyToAttack) {
            Attack();
            StartCoroutine(ParticleAttackCooldown());
        }
    }

    public void SetParticleAttack(EnemyParticleAttackSO enemyParticleAttack) {
        this.enemyParticleAttack = enemyParticleAttack;
    }

    private void Attack() {
        GameObject instantiatedGameObject = Instantiate(enemyParticleAttack.partcileAttackPrefab, transform.position, Quaternion.identity);

        particleAttack = instantiatedGameObject.GetComponent<ParticleSystem>();
        EnemyParticleAttackDamage particleDamage = instantiatedGameObject.GetComponent<EnemyParticleAttackDamage>();
        particleDamage.Init(enemyParticleAttack.particleDamage);

        particleAttack.Play();
    }

    private IEnumerator ParticleAttackCooldown() {
        float timer = 0f;
        readyToAttack = false;

        while (timer < enemyParticleAttack.attackCooldown) {
            timer += Time.deltaTime;

            yield return null;
        }

        readyToAttack = true;
    }
}
