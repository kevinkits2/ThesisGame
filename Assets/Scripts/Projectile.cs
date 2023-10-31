using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private float projectileSpeed;
    [SerializeField] private int projectileDamage;

    private bool isAwake;
    private Vector3 direction;


    public void Init(Vector3 direction, float lifeTime) {
        isAwake = true;
        this.direction = direction;

        Destroy(gameObject, lifeTime);
    }

    public void Update() {
        if (!isAwake) return;

        transform.position += direction * projectileSpeed * Time.deltaTime;
        transform.right = -direction;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable)) return;

        damageable.TakeDamage(projectileDamage);
        Destroy(gameObject);
    }
}
