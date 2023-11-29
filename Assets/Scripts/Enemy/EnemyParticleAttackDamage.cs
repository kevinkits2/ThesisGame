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

        particleSys.trigger.AddCollider(Player.Instance.GetShieldCollider());
    }

    private void OnParticleCollision(GameObject other) {
        int numCollisionEvents = particleSys.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++) {
            if (!collisionEvents[i].colliderComponent.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth)) continue;

            playerHealth.TakeDamage(particleDamage);
        }
    }

    /*private void OnParticleTrigger() {
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        int numEnter = particleSys.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        Debug.Log("test");

        for (int i = 0; i < numEnter; i++) {
            ParticleSystem.Particle particle = enter[i];

            Collider playerShieldCollider = Player.Instance.GetShieldCollider();

            if (playerShieldCollider.bounds.Contains(particle.position)) {
                particle.remainingLifetime = 0f;
                enter[i] = particle;
            }
        }

        particleSys.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }*/
}
