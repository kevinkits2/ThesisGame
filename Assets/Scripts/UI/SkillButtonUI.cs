using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public event Action<PlayerSkillSO> OnPointerOnButton;
    public event Action OnPointerOffButton;

    [SerializeField] private PlayerSkillSO playerSkillSO;

    private Image skillImage;


    private void Awake() {
        if (playerSkillSO == null) return;

        skillImage = GetComponent<Image>();
        skillImage.sprite = playerSkillSO.skillIcon;
    }

    public PlayerSkillSO GetPlayerSkillSO() {
        return playerSkillSO;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        OnPointerOnButton.Invoke(playerSkillSO);
    }

    public void OnPointerExit(PointerEventData eventData) {
        OnPointerOffButton.Invoke();
    }
}
