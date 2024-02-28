using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon {

    private const string ATTACK_TRIGGER = "Attack";

    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private GameObject swordHitbox;
    [SerializeField] private GameObject swordSlashPrefab;
    [SerializeField] private Transform swordSlashOrigin;

    private WeaponAnimator weaponAnimator;


    private void Awake() {
        weaponAnimator = GetComponentInChildren<WeaponAnimator>();
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    public void Attack() {
        weaponAnimator.PlayAttackAnim(DisableHitbox);
        //swordHitbox.SetActive(true);
        PlayerAttack.Instance.GetWeaponHitbox().GetComponent<PolygonCollider2D>().enabled = true;
        Instantiate(swordSlashPrefab, swordSlashOrigin);
    }

    public void DisableHitbox() {
        //swordHitbox.gameObject.SetActive(false);
        PlayerAttack.Instance.GetWeaponHitbox().GetComponent<PolygonCollider2D>().enabled = false;
    }

    public WeaponSO GetWeaponSO() {
        return weaponSO;
    }

    private void MouseFollowWithOffset() {
        Vector3 mousePos = Input.mousePosition;
        Vector2 playerScreenPoint = Camera.main.WorldToScreenPoint(Player.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
            PlayerAttack.Instance.GetWeaponContainer().rotation = Quaternion.Euler(0, -180, angle);
        }
        else {
            PlayerAttack.Instance.GetWeaponContainer().rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
