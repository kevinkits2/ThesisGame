using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "Weapon")]
public class WeaponSO : ScriptableObject {

    public GameObject weaponPrefab;
    public int weaponDamage;
    public float attackCooldown;
}
