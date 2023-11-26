using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    private const float PLAYER_LOOK_RIGHT_ROTATION = 0f;
    private const float PLAYER_LOOK_LEFT_ROTATION = 180f;


    private void FixedUpdate() {
        Vector3 mousePosition = InputManager.Instance.GetMouseWorldPosition();
        Vector3 lookDirection = mousePosition - transform.position;
        lookDirection.Normalize();

        if (lookDirection.x < 0) {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, PLAYER_LOOK_LEFT_ROTATION, 
                transform.localEulerAngles.z);
        }
        else {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, PLAYER_LOOK_RIGHT_ROTATION, 
                transform.localEulerAngles.z);
        }
    }
}
