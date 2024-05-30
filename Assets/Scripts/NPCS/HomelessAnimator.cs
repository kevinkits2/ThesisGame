using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomelessAnimator : MonoBehaviour {

    private const string MOVE_ANIM_PARAM = "Move";

    private Animator animator;
    private Rigidbody2D rb;


    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        animator.SetFloat(MOVE_ANIM_PARAM, rb.velocity.sqrMagnitude);
    }
}
