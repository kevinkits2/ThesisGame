using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteOrderChanger : MonoBehaviour {

    private const int ON_PLAYER_LAYER_ORDER = -5;
    private const int UNDER_PLAYER_LAYER_ORDER = 5;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private Transform playerTransform;
    private Vector3 directionToPlayer;


    private void Start() {
        playerTransform = Player.Instance.transform;
    }

    private void Update() {
        GetDirectionToPlayer();

        if (directionToPlayer.y > 0) {
            spriteRenderer.sortingOrder = UNDER_PLAYER_LAYER_ORDER;
        }
        else {
            spriteRenderer.sortingOrder = ON_PLAYER_LAYER_ORDER;
        }
    }

    private void GetDirectionToPlayer() {
        directionToPlayer = playerTransform.position - transform.position;
    }
}
