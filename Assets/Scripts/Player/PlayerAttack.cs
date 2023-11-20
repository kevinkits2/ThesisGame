using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private PlayerStats playerStats;
    private Vector3 shootDirection;
    private bool attackOnCooldown;


    private void Awake() {
        playerStats = GetComponent<PlayerStats>();
    }

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

        float attackCooldown = playerStats.GetAttackSpeed(Player.Instance.GetCurrentWeapon().reloadTime);
        yield return new WaitForSeconds(attackCooldown);

        attackOnCooldown = false;
    }
}
