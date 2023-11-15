using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldSkill : MonoBehaviour {

    [SerializeField] GameObject playerShield;

    private float duration;
    private float cooldown;

    private Coroutine cooldownCoroutine;


    private void Start() {
        Player.Instance.GetMovementComponent().OnDash += HandleDash;
    }

    private void HandleDash(object sender, PlayerMovement.OnDashEventArgs e) {
        if (cooldownCoroutine == null) {
            cooldownCoroutine = StartCoroutine(ShieldCooldown());
            StartCoroutine(ShieldDuration());
        }
    }

    public void Init(float duration, float cooldown) {
        this.duration = duration;
        this.cooldown = cooldown;
    }

    private IEnumerator ShieldDuration() {
        playerShield.SetActive(true);
        float timer = 0f;

        while (timer < duration) {
            timer += Time.deltaTime;

            yield return null;
        }

        playerShield.SetActive(false);
    }

    private IEnumerator ShieldCooldown() {
        float timer = 0f;

        while (timer < cooldown) {
            timer += Time.deltaTime;

            yield return null;
        }

        cooldownCoroutine = null;
    }
}
