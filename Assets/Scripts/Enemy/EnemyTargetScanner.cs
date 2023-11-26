using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyTargetScanner : MonoBehaviour {

    public event Action<Transform> OnTargetFound;
    public event Action OnTargetLost;

    [SerializeField] private float targetScanRadius;
    [SerializeField] private float scanFrequency;

    private float scanTimer;
    private bool targetFound;


    private void Update() {
        if (!targetFound) {
            ScanForTargetFound();
        }
        else {
            ScanForTargetLost();
        }
    }

    private void ScanForTargetFound() {
        scanTimer += Time.deltaTime;

        if (scanTimer < scanFrequency) return;

        if (Vector2.Distance(Player.Instance.transform.position, transform.position) <= targetScanRadius) {
            OnTargetFound?.Invoke(Player.Instance.transform);

            targetFound = true;
            scanTimer = 0f;
        }   
    }

    private void ScanForTargetLost() {
        scanTimer += Time.deltaTime;

        if (scanTimer < scanFrequency) return;

        if (Vector2.Distance(Player.Instance.transform.position, transform.position) > targetScanRadius) {
            OnTargetLost?.Invoke();

            targetFound = false;
            scanTimer = 0f;
        }
    }
}
