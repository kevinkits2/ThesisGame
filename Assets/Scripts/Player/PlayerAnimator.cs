using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private const string PLAYER_IS_MOVING = "isMoving";

    [SerializeField] private Animator playerAnimator;


    private void Update() {
        PlayMovingAnimation();
    }

    private void PlayMovingAnimation() {
        if (Player.Instance.IsMoving()) {
            playerAnimator.SetBool(PLAYER_IS_MOVING, true);
        }
        else {
            playerAnimator.SetBool(PLAYER_IS_MOVING, false);
        }
    }
}
