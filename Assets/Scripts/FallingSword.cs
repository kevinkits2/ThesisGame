using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSword : MonoBehaviour {

    [SerializeField] private Transform hitboxOrigin;
    [SerializeField] private Vector2 hitboxSize;

    private int damage;
    private bool isAwake;


    public void Init(int damage, float lifeTime) {
        this.damage = damage;
        isAwake = true;

        Destroy(gameObject, lifeTime);
    }

    private void Update() {
        if (!isAwake) return;

        DetectColliders();
    }

    public void DetectColliders() {
        foreach (Collider2D collider in Physics2D.OverlapBoxAll(hitboxOrigin.position, hitboxSize, 0f)) {
            if (!collider.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable)) continue;

            damageable.TakeDamage(damage);
        }
    }
}
