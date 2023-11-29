using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private EnemySO enemySO;
    [SerializeField] private float enemyMoveSpeed;


    private void Awake() {
        if (gameObject.TryGetComponent<EnemyProjectileAttack>(out EnemyProjectileAttack projectileAttack)) {
            projectileAttack.SetProjectileAttack(enemySO.projectileAttack);
        }

        if (gameObject.TryGetComponent<EnemyParticleAttack>(out EnemyParticleAttack particleAttack)) {
            particleAttack.SetParticleAttack(enemySO.particleAttack);
        }
    }

    public float GetMoveSpeed() {
        return enemyMoveSpeed;
    }

    public float GetExperiencePointsOnDeath() {
        return enemySO.experiencePointsOnDeath;
    }
}
