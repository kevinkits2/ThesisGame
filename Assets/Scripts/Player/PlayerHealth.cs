using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageFlash))]
public class PlayerHealth : MonoBehaviour {

    public event Action<int> OnTakeDamage;

    [SerializeField] private int maxHealth;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private bool canTakeDamage = true;
    private DamageFlash damageFlash;
    private Knockback knockback;
    private int currentHealth;


    private void Awake() {
        damageFlash = GetComponent<DamageFlash>();
        knockback = GetComponent<Knockback>();

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, Transform hitTransform) {
        if (Player.Instance.IsShieldActive()) return;
        if (!canTakeDamage) return;

        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(damageFlash.FlashRoutine());

        canTakeDamage = false;
        currentHealth -= damage;
        StartCoroutine(DamageRecoveryRoutine());

        OnTakeDamage?.Invoke(damage);

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    private IEnumerator DamageRecoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public int GetCurrentHealth() {
        return currentHealth;
    }
}
