using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour {

    public static SkillTreeUI Instance;

    public event Action<PlayerSkillSO> OnSkillButtonClicked; 

    [SerializeField] private Button[] skillButtons;
    [SerializeField] private GameObject skillDescriptionWindow;
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI skillDescriptionText;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("SkillTreeUI instance already exists!!!");
            return;
        }
        
        Instance = this;

        foreach (Button button in skillButtons) {
            SkillButtonUI skillButtonUI = button.GetComponent<SkillButtonUI>();

            skillButtonUI.OnPointerOnButton += HandlePointerOnButton;
            skillButtonUI.OnPointerOffButton += SkillButtonUI_OnPointerOffButton;

            button.onClick.AddListener(() => {
                OnSkillButtonClicked?.Invoke(skillButtonUI.GetPlayerSkillSO());
            });
        }
    }

    private void HandlePointerOnButton(PlayerSkillSO obj) {
        skillNameText.text = obj.skillName;
        skillDescriptionText.text = obj.skillDescription;

        skillDescriptionWindow.SetActive(true);
    }

    private void SkillButtonUI_OnPointerOffButton() {
        skillDescriptionWindow.SetActive(false);
    }
}
