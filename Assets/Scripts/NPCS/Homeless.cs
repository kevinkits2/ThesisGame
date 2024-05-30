using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Homeless : MonoBehaviour {

    [SerializeField] private Transform wanderingCenter;
    [SerializeField] private float wanderingDistance = 3f;
    [SerializeField] private float wanderingCooldown = 7f;
    [SerializeField] private float wanderTime = 2f;
    [SerializeField] private float moveSpeed = 0.2f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float wanderTimer = 0f;
    private Vector2 wanderPosition;
    private bool wanderingDisabled;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        wanderPosition = GetRandomPosition();
    }

    private void Update() {
        if (wanderTimer >= wanderingCooldown) {
            wanderPosition = GetRandomPosition();
            wanderTimer = 0f;
            wanderingDisabled = false;
        }
        else if (wanderTimer >= wanderTime) {
            wanderingDisabled = true;
        }

        wanderTimer += Time.deltaTime;
    }

    private void FixedUpdate() {
        if (wanderingDisabled) return;

        //Vector2 moveDirection = (Vector2)transform.position - wanderPosition;
        //rb.MovePosition((Vector2)transform.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));


        if (wanderPosition.x < 0) {
            spriteRenderer.flipX = true;
        }
        else if (wanderPosition.x > 0) {
            spriteRenderer.flipX = false;
        }
    }

    private void Move() {
        
    }

    private Vector2 GetRandomPosition() {
        float posX = UnityEngine.Random.Range(wanderingCenter.position.x - wanderingDistance, 
            wanderingCenter.position.x + wanderingDistance);
        float posY = UnityEngine.Random.Range(wanderingCenter.position.y - wanderingDistance, 
            wanderingCenter.position.y + wanderingDistance);

        return new Vector2(posX, posY);
    }
}
