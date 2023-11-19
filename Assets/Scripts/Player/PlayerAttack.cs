using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private Vector3 shootDirection;
    private bool attackOnCooldown;


    private void Start() {
        InputManager.Instance.OnAttackAction += HandleAttackAction;
    }

    private void Update() {
        GetShootDirectionFromPlayerNormalized();
    }

    private void HandleAttackAction(object sender, System.EventArgs e) {
        if (attackOnCooldown) return;

        Vector3 shootOrigin = Player.Instance.GetWeaponShootOrigin();
        Player.Instance.GetCurrentWeapon().Shoot(shootOrigin, shootDirection);

        StartCoroutine(AttackCooldown());
    }

    private void GetShootDirectionFromPlayerNormalized() {
        Vector3 mousePosition = InputManager.Instance.GetMouseWorldPosition();

        shootDirection = mousePosition - transform.position;
        shootDirection.Normalize();
    }

    private IEnumerator AttackCooldown() {
        attackOnCooldown = true;

        yield return new WaitForSeconds(Player.Instance.GetCurrentWeapon().reloadTime);

        attackOnCooldown = false;
    }
}
