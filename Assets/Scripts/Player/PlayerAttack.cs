using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Singleton<PlayerAttack> {

    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    [SerializeField] private WeaponSO defaultWeapon;
    [SerializeField] private Transform weaponContainer;
    [SerializeField] private GameObject weaponHitbox;

    private PlayerStats playerStats;
    private bool attackOnCooldown;


    protected override void Awake() {
        base.Awake();

        playerStats = GetComponent<PlayerStats>();
    }

    private void Start() {
        InputManager.Instance.OnAttackAction += HandleAttackAction;
        EquipWeapon(defaultWeapon);
    }

    public void EquipWeapon(WeaponSO weaponSO) {
        GameObject newWeapon = Instantiate(weaponSO.weaponPrefab, weaponContainer);
        CurrentActiveWeapon = newWeapon.GetComponent<MonoBehaviour>();

        PlayerDamageSource playerDamageSource = weaponHitbox.GetComponent<PlayerDamageSource>();
        playerDamageSource.UpdateDamageSource((CurrentActiveWeapon as IWeapon).GetWeaponSO());
    }

    public Transform GetWeaponContainer() {
        return weaponContainer;
    }

    public GameObject GetWeaponHitbox() {
        return weaponHitbox;
    }

    private void HandleAttackAction(object sender, System.EventArgs e) {
        if (attackOnCooldown) return;

        (CurrentActiveWeapon as IWeapon).Attack();
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown() {
        attackOnCooldown = true;

        yield return new WaitForSeconds((CurrentActiveWeapon as IWeapon).GetWeaponSO().attackCooldown);

        attackOnCooldown = false;
    }
}
