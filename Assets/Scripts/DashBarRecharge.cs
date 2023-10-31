using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBarRecharge : MonoBehaviour {

    [SerializeField] GameObject dashCooldownContainer;
    [SerializeField] Image dashBarSlider;

    private float dashCooldown;


    private void Awake() {
        dashCooldownContainer.SetActive(false);
    }

    private void Start() {
        PlayerMovement playerMovementComponent = Player.Instance.GetMovementComponent();
        playerMovementComponent.OnDash += HandleOnDash;
    }

    private void HandleOnDash(object sender, PlayerMovement.OnDashEventArgs e) {
        dashCooldown = e.dashCooldown;
        StartCoroutine(FillDashBar());
    }

    private IEnumerator FillDashBar() {
        dashCooldownContainer.SetActive(true);

        dashBarSlider.fillAmount = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < dashCooldown) {
            elapsedTime += Time.deltaTime;

            float currentFillAmount = Mathf.Lerp(0f, 1f, (elapsedTime / dashCooldown));
            dashBarSlider.fillAmount = currentFillAmount;

            yield return null;
        }

        dashCooldownContainer.SetActive(false);
    }
}
