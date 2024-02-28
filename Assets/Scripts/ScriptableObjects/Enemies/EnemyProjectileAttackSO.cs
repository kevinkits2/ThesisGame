using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttack")]
public class EnemyProjectileAttackSO : ScriptableObject {

    public GameObject projectilePrefab;

    public int projectileAmount;
    public float projectileSpread;
    public float projectileSpreadStartAngle;
    
    public int projectileDamage;
    public float projectileSpeed;

    public float projectileLifetime;
    public float projectileAttackCooldown;
}
