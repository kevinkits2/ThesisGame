using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour {

    private List<SkillType> activeSkills = new();

    private PlayerShieldSkill playerShieldSkillComponent;
    private PlayerFallingSwordsSkill playerFallingSwordsSkillComponent;


    private void Awake() {
        playerShieldSkillComponent = GetComponent<PlayerShieldSkill>();
        playerShieldSkillComponent.enabled = false;

        playerFallingSwordsSkillComponent = GetComponent<PlayerFallingSwordsSkill>();
        playerFallingSwordsSkillComponent.enabled = false;
    }

    private void Start() {
        GameUIManager.Instance.OnSkillButtonClicked += HandleSkillButtonClicked;
    }

    private void HandleSkillButtonClicked(PlayerSkillSO obj) {
        if (activeSkills.Contains(obj.skillType)) {
            UpgradeSkill(obj);
        }
        else {
            EnableSkill(obj);
        }
    }

    private void EnableSkill(PlayerSkillSO skill) {
        switch (skill.skillType) {
            case SkillType.Shield:
                playerShieldSkillComponent.enabled = true;
                playerShieldSkillComponent.Init(skill.skillDuration, skill.skillCooldownTime);

                break;

            case SkillType.FallingSwords:
                playerFallingSwordsSkillComponent.enabled = true;
                playerFallingSwordsSkillComponent.Init(skill.skillCooldownTime, skill.skillDamage, skill.prefabLifetime, 
                    skill.skillSpawnPrefab);
                break;
        }
    }

    private void UpgradeSkill(PlayerSkillSO skill) {

    }
}
