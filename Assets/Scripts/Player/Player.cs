using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public event Action<int> OnTakeDamage;

    [SerializeField] private WeaponSO defaultPlayerWeapon;
    [SerializeField] private Transform weaponContainer;

    private WeaponSO currentWeapon;
    private PlayerMovement playerMovementComponent;
    private PlayerDashGhostingEffect playerDashGhostingEffect;
    private PlayerHealth playerHealth;

    private float experiencePoints;


    private void Awake() {
        if (Instance != null) {
            return;
        }

        Instance = this;

        playerMovementComponent = GetComponent<PlayerMovement>();
        playerDashGhostingEffect = GetComponent<PlayerDashGhostingEffect>();
        playerDashGhostingEffect.enabled = false;

        playerHealth = GetComponent<PlayerHealth>();
        playerHealth.OnTakeDamage += HandleTakeDamage;

        EquipWeapon(defaultPlayerWeapon);
    }

    private void HandleTakeDamage(int obj) => OnTakeDamage.Invoke(obj);

    private void EquipWeapon(WeaponSO weapon) {
        Instantiate(weapon.weaponPrefab, weaponContainer);

        currentWeapon = weapon;
    }

    public void ReceiveExperiencePoints(float amount) {
        experiencePoints += amount;
    }

    public bool IsMoving() => playerMovementComponent.IsMoving();

    public void PlayDashGhostEffect() {
        playerDashGhostingEffect.enabled = true;
    }

    public void StopDashGhostEffect() {
        playerDashGhostingEffect.enabled = false;
    }

    public PlayerMovement GetMovementComponent() {
        return playerMovementComponent;
    }

    public WeaponSO GetCurrentWeapon() {
        return currentWeapon;
    }

    public Vector3 GetWeaponShootOrigin() {
        return weaponContainer.transform.position;
    }

    public int GetMaxHealth() => playerHealth.GetMaxHealth();
    public int GetCurrentHealth() => playerHealth.GetCurrentHealth();
}
