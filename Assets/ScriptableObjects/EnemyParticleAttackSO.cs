using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyParticleAttack")]
public class EnemyParticleAttackSO : ScriptableObject {

    public GameObject partcileAttackPrefab;
    public int particleDamage;
    public float attackCooldown;

}
