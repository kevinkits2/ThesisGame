using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class WeaponSO : ScriptableObject {

    public GameObject weaponPrefab;


    public void SpawnWeapon(Transform parent) {
        Instantiate(weaponPrefab, parent);
    }
}
