using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSword : MonoBehaviour {

    [SerializeField] private Transform swordTip;
    [SerializeField] private GameObject dustParticles;
    [SerializeField] private Sprite swordInGroundSprite;
    [SerializeField] private Transform hitboxOrigin;
    [SerializeField] private float hitboxRadius;

    private int damage;


    public void Init(int damage, float lifeTime) {
        this.damage = damage;

        Destroy(gameObject, lifeTime);
    }

    public void DetectColliders() {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(hitboxOrigin.position, hitboxRadius)) {
            if (!collider.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable)) continue;

            damageable.TakeDamage(damage);
        }
    }
}
