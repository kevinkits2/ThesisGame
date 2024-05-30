using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {

    [SerializeField] private Image healthBar;


    private void Start() {
        Player.Instance.OnTakeDamage += HandleTakeDamage;
    }

    private void HandleTakeDamage(int damage) {
        healthBar.fillAmount -= (float)damage / Player.Instance.GetMaxHealth();
    }
}
