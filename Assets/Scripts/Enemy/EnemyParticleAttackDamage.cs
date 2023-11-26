using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EnemyParticleAttackDamage : MonoBehaviour {

    private ParticleSystem particleSys;
    private List<ParticleCollisionEvent> collisionEvents;
    private int particleDamage;


    private void Awake() {
        particleSys = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    public void Init(int damage) {
        particleDamage = damage;
    }

    private void OnParticleCollision(GameObject other) {
        int numCollisionEvents = particleSys.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++) {
            if (!collisionEvents[i].colliderComponent.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth)) continue;

            playerHealth.TakeDamage(particleDamage);
        }
    }
}
