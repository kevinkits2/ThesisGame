using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageFlash))]
public class EnemyHealth : MonoBehaviour, IDamageable {

    [SerializeField] private int maxHealth;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private GameObject coinSpawnerPrefab;

    private Enemy enemy;
    private DamageFlash damageFlash;
    private Knockback knockback;
    private int currentHealth;


    private void Awake() {
        damageFlash = GetComponent<DamageFlash>();
        knockback = GetComponent<Knockback>();
        enemy = GetComponent<Enemy>();

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        knockback.GetKnockedBack(Player.Instance.transform, knockBackThrust);
        StartCoroutine(damageFlash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine() {
        yield return new WaitForSeconds(damageFlash.GetRestoreMatTime());
        DetectDeath();
    }

    private void DetectDeath() {
        if (currentHealth <= 0) {
            //Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Instantiate(coinSpawnerPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
