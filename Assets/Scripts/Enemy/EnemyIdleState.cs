using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : MonoBehaviour {

    public event Action<Transform> OnTargetFound;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.TryGetComponent<Player>(out Player player)) return;

        OnTargetFound.Invoke(player.transform);
    }
}
