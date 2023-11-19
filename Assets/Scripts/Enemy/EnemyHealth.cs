using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageFlash))]
public class EnemyHealth : MonoBehaviour, IDamageable {

    public event EventHandler OnTakeDamage;

    [SerializeField] private int maxHealth;

    private Enemy enemy;
    private DamageFlash damageFlash;
    private int currentHealth;


    private void Awake() {
        damageFlash = GetComponent<DamageFlash>();
        enemy = GetComponent<Enemy>();

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        damageFlash.CallDamageFlash();

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }

        OnTakeDamage.Invoke(this, EventArgs.Empty);
    }
}
