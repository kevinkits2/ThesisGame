using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    private const int DEXTERITY_DIVISOR = 5;
    private const float FASTEST_ATTACK_SPEED = 0.1f;

    public event Action<PlayerLevelRewardsSO> OnLevelUp;

    [SerializeField] private int strengthDefault;
    [SerializeField] private int dexterityDefault;
    [SerializeField] private int wisdomDefault;

    [SerializeField] private PlayerLevel[] playerLevel;

    public int Strength { get; private set; }
    public int Dexterity { get; private set; }
    public int Wisdom { get; private set; }

    private int currentLevel;
    private float experiencePoints;
    private int currentAvailableStatPoints;


    private void Awake() {
        Strength = strengthDefault;
        Dexterity = dexterityDefault;
        Wisdom = wisdomDefault;

        currentLevel = 1;
        currentAvailableStatPoints = GetPlayerLevel().rewards.statPointsGained;
    }

    private void Start() {
        PlayerStatsUI.Instance.OnPlayerStatIncreased += HandlePlayerStatIncreased;
        OnLevelUp?.Invoke(GetPlayerLevel().rewards);
    }

    private void HandlePlayerStatIncreased(PlayerStat stat, Action callback) {
        switch (stat) {
            case PlayerStat.Strength:
                Strength++;
                break;

            case PlayerStat.Dexterity:
                Dexterity++;
                break;

            case PlayerStat.Wisdom:
                Wisdom++;
                break;
        }

        currentAvailableStatPoints--;
        callback();
    }

    public float GetAttackSpeed(float defaultAttackSpeed) {
        return Mathf.Clamp(defaultAttackSpeed - ((float)Dexterity / DEXTERITY_DIVISOR), FASTEST_ATTACK_SPEED, defaultAttackSpeed);
    }

    public void GainExperiencePoints(float points) {
        experiencePoints += points;

        if (GetPlayerLevel().experienceToNextLevel <= experiencePoints) {
            currentLevel++;
            currentAvailableStatPoints += GetPlayerLevel().rewards.statPointsGained;
            OnLevelUp?.Invoke(GetPlayerLevel().rewards);
        }
    }

    private PlayerLevel GetPlayerLevel() {
        return playerLevel[currentLevel];
    }

    public int GetAvailableStatPoints() {
        return currentAvailableStatPoints;
    }
}

public enum PlayerStat {
    Strength,
    Dexterity,
    Wisdom,
}
