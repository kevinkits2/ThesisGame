using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;

public class WeaponRotation : MonoBehaviour {

    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer weaponSpriteRenderer;


    private void Start() {
        weaponSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update() {
        Vector3 mousePosition = InputManager.Instance.GetMouseWorldPosition();
        Vector3 lookDirection = (mousePosition - transform.position).normalized;

        transform.right = lookDirection;

        // Temporary bugfix (for some reason when transform.right == (-1, 0), z rotation becomes 0 and y becomes 180)
        // it should be z = 180 and y = 0
        if (transform.right == Vector3.left) {
            Vector3 rotation = transform.eulerAngles;
            rotation.y = 0;
            rotation.z = 180;

            transform.eulerAngles = rotation;
        }

        Vector2 scale = transform.localScale;
        if (lookDirection.x < 0) {
            scale.y = -1;
        }
        else {
            scale.y = 1;
        }

        transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180) {
            weaponSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder - 1;
        }
        else {
            weaponSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder + 1;
        }
    }
}
