using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAttackState : MonoBehaviour {

    private const float PLAYER_COLLIDER_COMPENSATION = 0.5f;

    public event EventHandler OnAttack;
    public event EventHandler OnTargetLost;

    [SerializeField] private float attackRange;
    private Transform target;


    private void OnEnable() {
        //Play Attack animation
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

    private void Update() {
        if (target == null) return;

        if (Vector3.Distance(target.position, transform.position) > attackRange + PLAYER_COLLIDER_COMPENSATION) {
            OnTargetLost.Invoke(this, EventArgs.Empty);
        }
    }

}
