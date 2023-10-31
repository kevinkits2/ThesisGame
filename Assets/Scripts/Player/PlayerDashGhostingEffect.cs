using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashGhostingEffect : MonoBehaviour {

    [SerializeField] private GameObject ghostSprite;
    [SerializeField] private float ghostFadeTime;
    [SerializeField] private float ghostSpawnFrequency;

    private float lastGhostTime;

    private void OnEnable() {
        lastGhostTime = 0f;
    }

    private void Update() {
        if (lastGhostTime >= ghostSpawnFrequency) {
            GameObject instantiatedGhost = Instantiate(ghostSprite, transform.position, transform.rotation);
            if (!instantiatedGhost.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer)) {
                Debug.LogError("Couldn't get sprite renderer from instantiated ghost");
                return;
            }

            StartCoroutine(FadeOutGhost(spriteRenderer));
        }

        lastGhostTime += Time.deltaTime;
    }

    private IEnumerator FadeOutGhost(SpriteRenderer spriteRenderer) {
        float fadeOutTimer = 0f;

        while (fadeOutTimer <= ghostFadeTime) {
            Color newAlpha = spriteRenderer.color;
            newAlpha.a -= ghostFadeTime * Time.deltaTime;
            spriteRenderer.color = newAlpha;

            fadeOutTimer += Time.deltaTime;

            yield return null;
        }

        Destroy(spriteRenderer.gameObject);
    }
}
