using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyParticleAttack : MonoBehaviour {

    private ParticleSystem particleAttack;
    private EnemyTargetScanner enemyTargetScanner;


    private void Start() {
        enemyTargetScanner = GetComponent<EnemyTargetScanner>();

        enemyTargetScanner.OnTargetFound += HandleTargetFound;
        enemyTargetScanner.OnTargetLost += HandleTargetLost;
    }

    private void HandleTargetLost() {
        particleAttack.Stop();
    }

    private void HandleTargetFound(Transform obj) {
        particleAttack.Play();
    }

    public void SetParticleAttack(EnemyParticleAttackSO enemyParticleAttack) {
        GameObject instantiatedGameObject = Instantiate(enemyParticleAttack.partcileAttackPrefab, transform);

        particleAttack = instantiatedGameObject.GetComponent<ParticleSystem>();
        EnemyParticleAttackDamage particleDamage = instantiatedGameObject.GetComponent<EnemyParticleAttackDamage>();
        particleDamage.Init(enemyParticleAttack.particleDamage);
    }
}
