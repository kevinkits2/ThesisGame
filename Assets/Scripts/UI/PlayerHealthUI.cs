using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI maxHealthText;
    [SerializeField] private Image healthBar;


    private void Start() {
        Player.Instance.OnTakeDamage += HandleTakeDamage;

        maxHealthText.text = Player.Instance.GetMaxHealth().ToString();
        currentHealthText.text = Player.Instance.GetMaxHealth().ToString();
    }

    private void HandleTakeDamage(int damage) {
        currentHealthText.text = (Player.Instance.GetCurrentHealth() - damage).ToString();
        healthBar.fillAmount -= (float)damage / Player.Instance.GetMaxHealth();
    }
}
