using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHelper : MonoBehaviour {

    public event EventHandler OnAttackPerformed;


    public void TriggerAttackPerformed() {
        OnAttackPerformed?.Invoke(this, EventArgs.Empty);
    }

    public void DestroyOnAnimationFinished() {
        Destroy(gameObject);
    }
}
