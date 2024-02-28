using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    public bool FacingLeft { get { return facingLeft; } }
    private bool facingLeft;

    [SerializeField] private SpriteRenderer spriteRenderer;

    
    private void FixedUpdate() {
        Vector3 mousePosition = InputManager.Instance.GetMouseWorldPosition();
        //Vector3 lookDirection = mousePosition - transform.position;
        //lookDirection.Normalize();

        if (mousePosition.x < transform.position.x) {
            spriteRenderer.flipX = true;
            facingLeft = true;
        }
        else {
            spriteRenderer.flipX = false;
            facingLeft = false;
        }

        /*if (lookDirection.x < 0) {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, PLAYER_LOOK_LEFT_ROTATION, 
                transform.localEulerAngles.z);
        }
        else {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, PLAYER_LOOK_RIGHT_ROTATION, 
                transform.localEulerAngles.z);
        }*/
    }
}
