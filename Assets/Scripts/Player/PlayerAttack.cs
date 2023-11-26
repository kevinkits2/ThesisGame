using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private const string WEAPON_ATTACK = "Attack";

    private Animator weaponAnimator;
    private PlayerStats playerStats;
    private Vector3 shootDirection;
    private bool attackOnCooldown;
    private WeaponSO currentWeapon;


    private void Awake() {
        playerStats = GetComponent<PlayerStats>();
    }

    private void Start() {
        InputManager.Instance.OnAttackAction += HandleAttackAction;
        currentWeapon = Player.Instance.GetCurrentWeapon();
        weaponAnimator = Player.Instance.GetWeaponContainer().GetComponentInChildren<Animator>();
    }

    private void Update() {
        GetShootDirectionFromPlayerNormalized();
    }

    private void HandleAttackAction(object sender, System.EventArgs e) {
        if (attackOnCooldown) return;

        if (currentWeapon.hasProjectiles) {
            Vector3 shootOrigin = Player.Instance.GetWeaponShootOrigin();
            Player.Instance.GetCurrentWeapon().Shoot(shootOrigin, shootDirection);
        }

        weaponAnimator.SetTrigger(WEAPON_ATTACK);

        StartCoroutine(AttackCooldown());
    }

    private void GetShootDirectionFromPlayerNormalized() {
        Vector3 mousePosition = InputManager.Instance.GetMouseWorldPosition();

        shootDirection = mousePosition - transform.position;
        shootDirection.Normalize();
    }

    private IEnumerator AttackCooldown() {
        attackOnCooldown = true;

        float attackCooldown = playerStats.GetAttackSpeed(currentWeapon.reloadTime);
        yield return new WaitForSeconds(attackCooldown);

        attackOnCooldown = false;
    }
}
