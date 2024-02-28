using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IBreakable {

    private const string ANIMATOR_BREAK_TRIGGER = "Break";

    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Break() {
        if (animator != null) {
            animator.SetTrigger(ANIMATOR_BREAK_TRIGGER);
        }
        else {
            Destroy(gameObject);
        }
    }
}
