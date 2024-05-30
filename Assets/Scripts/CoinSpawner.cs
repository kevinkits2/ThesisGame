using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

    [SerializeField] private int coinAmount = 5;
    [SerializeField] private float projectileRange = 5f;
    [SerializeField] private GameObject coinPrefab;


    private void Start() {
        SpawnCoins();
    }

    public void SpawnCoins() {
        float startAngle, currentAngle, angleStep;
        angleStep = 360 / coinAmount;

        if (coinAmount % 2 == 1) {
            startAngle = angleStep / coinAmount;
        }
        else {
            startAngle = 0f;
        }

        currentAngle = startAngle;

        for (int i = 0; i < coinAmount; i++) {
            GameObject newCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            Coin coin = newCoin.GetComponent<Coin>();

            Vector2 targetDirection = transform.position -
                Quaternion.AngleAxis(currentAngle, Vector3.forward) * Vector3.right * projectileRange;

            coin.Init(targetDirection);

            currentAngle += angleStep;
        }
    }
}
