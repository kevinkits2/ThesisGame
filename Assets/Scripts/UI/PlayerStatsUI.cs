using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour {

    public static PlayerStatsUI Instance;

    public event Action<PlayerStat, Action> OnPlayerStatIncreased;

    [SerializeField] private TextMeshProUGUI strengthNumber; 
    [SerializeField] private Button strengthIncreaseButton;

    [SerializeField] private TextMeshProUGUI dexterityNumber;
    [SerializeField] private Button dexterityIncreaseButton;

    [SerializeField] private TextMeshProUGUI wisdomNumber;
    [SerializeField] private Button wisdomIncreaseButton;

    [SerializeField] private TextMeshProUGUI availablePointsNumber;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("PlayerStatsUI instance already exists!!!");
            return;
        }

        Instance = this;

        strengthIncreaseButton.onClick.AddListener(() => {
            if (Player.Instance.GetAvailableStatPoints() <= 0) return;

            OnPlayerStatIncreased?.Invoke(PlayerStat.Strength, () => {
                availablePointsNumber.text = Player.Instance.GetAvailableStatPoints().ToString();
            });

            strengthNumber.text = Player.Instance.GetStrength().ToString();
        });

        dexterityIncreaseButton.onClick.AddListener(() => {
            if (Player.Instance.GetAvailableStatPoints() <= 0) return;

            OnPlayerStatIncreased?.Invoke(PlayerStat.Dexterity, () => {
                availablePointsNumber.text = Player.Instance.GetAvailableStatPoints().ToString();
            });

            dexterityNumber.text = Player.Instance.GetDexterity().ToString();
        });

        wisdomIncreaseButton.onClick.AddListener(() => {
            if (Player.Instance.GetAvailableStatPoints() <= 0) return;

            OnPlayerStatIncreased?.Invoke(PlayerStat.Wisdom, () => {
                availablePointsNumber.text = Player.Instance.GetAvailableStatPoints().ToString();
            });

            wisdomNumber.text = Player.Instance.GetWisdom().ToString();
        });
    }

    private void Start() {
        Player.Instance.OnLevelUp += HandleLevelUp;
    }

    private void HandleLevelUp(PlayerLevelRewardsSO rewards) {
        availablePointsNumber.text = Player.Instance.GetAvailableStatPoints().ToString();
    }
}
