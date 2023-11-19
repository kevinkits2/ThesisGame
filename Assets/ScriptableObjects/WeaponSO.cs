using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class WeaponSO : ScriptableObject {

    public GameObject weaponPrefab;
    public ProjectileSO projectileSO;
    public int weaponDamage;
    public float reloadTime;

    public void Shoot(Vector3 origin, Vector3 shootDirection) {
        GameObject instantiatedProjectile = Instantiate(projectileSO.projectilePrefab, origin, Quaternion.identity);
        if (!instantiatedProjectile.TryGetComponent<Projectile>(out Projectile projectile)) return;

        projectile.Init(shootDirection, projectileSO.projectileSpeed, projectileSO.projectileDamage, projectileSO.projectileLifetime);
    }
}
