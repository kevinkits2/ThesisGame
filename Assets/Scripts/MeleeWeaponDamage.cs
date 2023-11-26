using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponDamage : MonoBehaviour {

    [SerializeField] private AnimationEventHelper animationEventHelper;
    [SerializeField] private Transform hitboxOrigin;
    [SerializeField] private float hitboxRadius;


    private void Start() {
        animationEventHelper.OnAttackPerformed += HandleAttackPerformed;
    }

    private void HandleAttackPerformed(object sender, System.EventArgs e) {
        DetectColliders();
    }

    public void DetectColliders() {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(hitboxOrigin.position, hitboxRadius)) {
            if (!collider.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable)) continue;

            damageable.TakeDamage(Player.Instance.GetCurrentWeapon().weaponDamage);
        }
    }
}
