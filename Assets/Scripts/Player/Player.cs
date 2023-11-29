using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public event Action<int> OnTakeDamage;
    public event Action<PlayerLevelRewardsSO> OnLevelUp;

    [SerializeField] private WeaponSO defaultPlayerWeapon;
    [SerializeField] private Transform weaponContainer;

    private WeaponSO currentWeapon;
    private PlayerMovement playerMovementComponent;
    private PlayerDashGhostingEffect playerDashGhostingEffect;
    private PlayerHealth playerHealth;
    private PlayerStats playerStats;
    private PlayerShieldSkill playerShieldSkill;


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

        playerStats = GetComponent<PlayerStats>();
        playerShieldSkill = GetComponent<PlayerShieldSkill>();

        EquipWeapon(defaultPlayerWeapon);
    }

    private void Start() {
        playerStats.OnLevelUp += HandleOnLevelUp;
    }

    private void HandleOnLevelUp(PlayerLevelRewardsSO obj) {
        OnLevelUp?.Invoke(obj);
    }

    private void HandleTakeDamage(int obj) => OnTakeDamage?.Invoke(obj);

    private void EquipWeapon(WeaponSO weapon) {
        GameObject instantiatedWeapon = Instantiate(weapon.weaponPrefab, weaponContainer);
        instantiatedWeapon.transform.position = weapon.weaponSpawnOffset;

        currentWeapon = weapon;
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

    public void GainExperiencePoints(float points) => playerStats.GainExperiencePoints(points);

    public int GetAvailableStatPoints() => playerStats.GetAvailableStatPoints();

    public int GetStrength() => playerStats.Strength;

    public int GetDexterity() => playerStats.Dexterity;

    public int GetWisdom() => playerStats.Wisdom;

    public CircleCollider2D GetShieldCollider() => playerShieldSkill.GetShieldCollider();

    public bool IsShieldActive() => playerShieldSkill.IsShieldActive();

    public Transform GetWeaponContainer() {
        return weaponContainer;
    }
}
