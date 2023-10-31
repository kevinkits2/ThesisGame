using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {

    private DamageFlash damageFlash;


    private void Awake() {
        damageFlash = GetComponent<DamageFlash>();
    }

    public void TakeDamage(int damage) {
        damageFlash.CallDamageFlash();
    }
}
