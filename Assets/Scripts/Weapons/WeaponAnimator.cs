using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour {

    private const string ATTACK_TRIGGER = "Attack";

    private Animator animator;
    private Action onAttackAnimationDone;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void PlayAttackAnim(Action onAttackAnimationDone) {
        animator.SetTrigger(ATTACK_TRIGGER);
        this.onAttackAnimationDone = onAttackAnimationDone;
    }

    public void DoneAttackingAnimEvent() {
        onAttackAnimationDone?.Invoke();
        onAttackAnimationDone = null;
    }
}
