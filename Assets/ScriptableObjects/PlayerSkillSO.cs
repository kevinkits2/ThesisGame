using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkill")]
public class PlayerSkillSO : ScriptableObject {

    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;
    public bool skillUnlocked;
    public bool skillAvailableToUnlock;

}
