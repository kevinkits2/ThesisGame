using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

    private const string ENEMY_IS_MOVING = "isMoving";

    [SerializeField] private Animator enemyAnimator;


    public void PlayIdleAnimation() {
        enemyAnimator.SetBool(ENEMY_IS_MOVING, false);
    }

    public void PlayFollowAnimation() {
        enemyAnimator.SetBool(ENEMY_IS_MOVING, true);
    }

    public void PlayAttackAnimation() {

    }
}
