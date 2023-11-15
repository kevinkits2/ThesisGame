using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float attackCooldownTime;

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
        
        GameObject instantiatedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        if (!instantiatedProjectile.TryGetComponent<Projectile>(out Projectile projectile)) return;

        projectile.Init(shootDirection);
        StartCoroutine(AttackCooldown());
    }

    private void GetShootDirectionFromPlayerNormalized() {
        Vector3 mousePosition = InputManager.Instance.GetMouseWorldPosition();

        shootDirection = mousePosition - transform.position;
        shootDirection.Normalize();
    }

    private IEnumerator AttackCooldown() {
        attackOnCooldown = true;

        yield return new WaitForSeconds(attackCooldownTime);

        attackOnCooldown = false;
    }
}
