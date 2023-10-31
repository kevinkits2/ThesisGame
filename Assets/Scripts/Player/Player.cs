using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    private PlayerMovement playerMovementComponent;
    private PlayerDashGhostingEffect playerDashGhostingEffect;


    private void Awake() {
        if (Instance != null) {
            return;
        }

        Instance = this;

        playerMovementComponent = GetComponent<PlayerMovement>();
        playerDashGhostingEffect = GetComponent<PlayerDashGhostingEffect>();
        playerDashGhostingEffect.enabled = false;
    }

    public bool IsMoving() => playerMovementComponent.IsMoving();

    public void PlayDashGhostEffect() {
        playerDashGhostingEffect.enabled = true;
    }

    public void StopDashGhostEffect() {
        playerDashGhostingEffect.enabled = false;
    }

    public PlayerMovement GetMovementComponent() {
        return playerMovementComponent;
    }
}
