using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField] private float duration = 0.2f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private GameObject coinShadowPrefab;

    private bool hasLanded;
    private GameObject coinShadow;


    public void Init(Vector3 targetPos) {
        coinShadow = Instantiate(coinShadowPrefab, transform.position + new Vector3(0, -0.33f, 0), Quaternion.identity);
        Vector3 coinShadowStartPos = coinShadow.transform.position;

        StartCoroutine(CoinCurveRoutine(transform.position, targetPos));
        StartCoroutine(MoveCoinShadow(coinShadow, coinShadowStartPos, targetPos));
    }

    private IEnumerator CoinCurveRoutine(Vector3 startPos, Vector3 endPos) {
        float timePassed = 0f;

        while (timePassed < duration) {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPos, endPos, linearT) + new Vector2(0f, height);

            yield return null;
        }

        hasLanded = true;
    }

    private IEnumerator MoveCoinShadow(GameObject coinShadow, Vector3 startPos, Vector3 endPos) {
        float timePassed = 0f;

        while (timePassed < duration) {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;

            coinShadow.transform.position = Vector2.Lerp(startPos, endPos, linearT);

            yield return null;
        }
    }

private void OnTriggerEnter2D(Collider2D collision) {
        if (!hasLanded) return;
        if (!collision.gameObject.TryGetComponent<Player>(out Player player)) return;

        PlayerGoldManager.Instance.AddGold(1);
        Destroy(coinShadow);
        Destroy(gameObject);
    }
}
