using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class LaunchLandSplatter : MonoBehaviour {

    private SpriteFade spriteFade;


    private void Awake() {
        spriteFade = GetComponent<SpriteFade>();
    }

    private void Start() {
        StartCoroutine(spriteFade.SlowFadeRoutine());

        Invoke("DisableCollider", 0.2f);
    }

    private void DisableCollider() {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(1, transform);
    }
}
