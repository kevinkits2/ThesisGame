using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy")]
public class EnemySO : ScriptableObject {

    public GameObject enemyPrefab;

    public EnemyProjectileAttackSO projectileAttack;
    public EnemyParticleAttackSO particleAttack;

    public int enemyMaxHealth;
    public float enemyMoveSpeed;
    public float experiencePointsOnDeath;

}
