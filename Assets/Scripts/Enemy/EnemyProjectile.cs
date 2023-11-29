using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask shieldLayer;

    private float projectileSpeed;
    private int projectileDamage;

    private bool isAwake;
    private Vector3 direction;


    public void Init(Vector3 direction, float projectileSpeed, int projectileDamage, float lifeTime = 5f) {
        isAwake = true;
        this.direction = direction;
        this.projectileSpeed = projectileSpeed;
        this.projectileDamage = projectileDamage;

        Destroy(gameObject, lifeTime);
    }

    public void Update() {
        if (!isAwake) return;

        transform.position += direction * projectileSpeed * Time.deltaTime;
        transform.right = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if ((wallLayer.value & (1 << collision.transform.gameObject.layer)) > 0) {
            Destroy(gameObject);
            return;
        }

        if ((shieldLayer.value & (1 << collision.transform.gameObject.layer)) > 0) {
            Destroy(gameObject);
            return;
        }

        if (!collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth)) return;

        playerHealth.TakeDamage(projectileDamage);
        Destroy(gameObject);
    }
}
