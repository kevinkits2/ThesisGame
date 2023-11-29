using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingSwordsSkill : MonoBehaviour {

    private const float SPAWN_DISTANCE = 3f;

    [SerializeField] private float[] swordAngles = new float[5];

    private GameObject fallingSwordPrefab;
    private float cooldown;
    private int damage;
    private float lifeTime;

    private void HandleFallingSwordsAction(object sender, System.EventArgs e) {
        Perform();
    }

    public void Init(float cooldown, int damage, float lifeTime, GameObject prefab) {
        InputManager.Instance.OnFallingSwordsAction += HandleFallingSwordsAction;

        this.cooldown = cooldown;
        this.damage = damage;
        this.lifeTime = lifeTime;
        fallingSwordPrefab = prefab;
    }

    private void Perform() {
        foreach (float swordAngle in swordAngles) {
            Vector2 spawnPosition = Quaternion.Euler(0f, 0f, swordAngle) * (transform.up * SPAWN_DISTANCE);
            spawnPosition += (Vector2)transform.position;

            GameObject instantiatedGameObject = Instantiate(fallingSwordPrefab, spawnPosition, Quaternion.identity);
            FallingSword fallingSword = instantiatedGameObject.GetComponent<FallingSword>();

            fallingSword.Init(damage, lifeTime);
        }
    }
}
