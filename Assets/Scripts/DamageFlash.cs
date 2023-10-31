using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour {

    private const string FLASH_COLOR = "_FlashColor";
    private const string FLASH_AMOUNT = "_FlashAmount";

    [ColorUsage(true, true)]
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashTime = 0.25f;
    [SerializeField] private AnimationCurve flashSpeedCurve;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private Material material;


    private void Awake() {
        material = spriteRenderer.material;
    }

    public void CallDamageFlash() {
        StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher() {
        SetFlashColor();

        float elapsedTime = 0f;

        while (elapsedTime < flashTime) {
            elapsedTime += Time.deltaTime;

            float currentFlashAmount = Mathf.Lerp(1f, flashSpeedCurve.Evaluate(elapsedTime), (elapsedTime / flashTime)) ;
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    private void SetFlashColor() {
        material.SetColor(FLASH_COLOR, flashColor);
    }

    private void SetFlashAmount(float amount) {
        material.SetFloat(FLASH_AMOUNT, amount);
    }
}
