using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkill")]
public class PlayerSkillSO : ScriptableObject {

    public SkillType skillType;

    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;
    public bool skillUnlocked;
    public bool skillAvailableToUnlock;

    public GameObject skillSpawnPrefab;
    public float prefabLifetime;
    public int skillDamage;
    public float skillCooldownTime;
    public float skillDuration;

}

public enum SkillType {
    Shield,
    FallingSwords,
}
