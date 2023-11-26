using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageFlash))]
public class PlayerHealth : MonoBehaviour {

    public event Action<int> OnTakeDamage;

    [SerializeField] private int maxHealth;

    private DamageFlash damageFlash;
    private int currentHealth;


    private void Awake() {
        damageFlash = GetComponent<DamageFlash>();

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        damageFlash.CallDamageFlash();

        OnTakeDamage?.Invoke(damage);

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public int GetCurrentHealth() {
        return currentHealth;
    }
}
