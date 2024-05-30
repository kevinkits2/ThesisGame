using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSource : MonoBehaviour {

    private int damageAmount;
    private GameObject weaponHitEffect;


    private void Start() {
        MonoBehaviour currentActiveWeapon = PlayerAttack.Instance.CurrentActiveWeapon;

        if (currentActiveWeapon == null) return;

        damageAmount = (currentActiveWeapon as IWeapon).GetWeaponSO().weaponDamage;
    }

    public void UpdateDamageSource(WeaponSO weaponSO) {
        damageAmount = weaponSO.weaponDamage;
        weaponHitEffect = weaponSO.weaponHitEffect;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth)) {
            enemyHealth.TakeDamage(damageAmount);

            GameObject newHitEffect = Instantiate(weaponHitEffect, collision.transform.position, Quaternion.identity);
            SpriteRenderer spriteRenderer = newHitEffect.GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = !PlayerRotation.Instance.FacingLeft;
        }
        if (collision.gameObject.TryGetComponent<Destructible>(out Destructible destructible)) {
            destructible.Destruct();
        }
    }

}
