using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSource : MonoBehaviour {

    private int damageAmount;


    private void Start() {
        MonoBehaviour currentActiveWeapon = PlayerAttack.Instance.CurrentActiveWeapon;

        if (currentActiveWeapon == null) return;

        damageAmount = (currentActiveWeapon as IWeapon).GetWeaponSO().weaponDamage;
    }

    public void UpdateDamageSource(WeaponSO weaponSO) {
        damageAmount = weaponSO.weaponDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth)) {
            enemyHealth.TakeDamage(damageAmount);
        }
        if (collision.gameObject.TryGetComponent<Destructible>(out Destructible destructible)) {
            destructible.Destruct();
        }
    }

}
