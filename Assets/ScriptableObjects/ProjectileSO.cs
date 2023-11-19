using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile")]
public class ProjectileSO : ScriptableObject {

    public GameObject projectilePrefab;

    public int projectileDamage;
    public float projectileSpeed;

    public float projectileLifetime;

}
