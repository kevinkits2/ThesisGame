using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour {

    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultMatTime = 0.2f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Material defaultMat;


    private void Awake() {
        defaultMat = spriteRenderer.material;
    }

    public float GetRestoreMatTime() {
        return restoreDefaultMatTime;
    }

    public IEnumerator FlashRoutine() {
        spriteRenderer.material = whiteFlashMat;

        yield return new WaitForSeconds(restoreDefaultMatTime);

        spriteRenderer.material = defaultMat;
    }
}
