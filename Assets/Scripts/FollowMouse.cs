using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    private void Update() {
        Vector3 mousePosition = InputManager.Instance.GetMouseWorldPosition();
        Vector3 lookDirection = mousePosition - transform.position;
        lookDirection.Normalize();

        transform.up = lookDirection;
    }
}
