using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour, IInteractable {

    [SerializeField] private GameObject anvilOutline;
    [SerializeField] private GameObject interactUI;

    private bool readyToInteract = false;


    private void Start() {
        InputManager.Instance.OnInteractPerformed += (_, _) => Interact();
    }

    public void Interact() {
        if (!readyToInteract) return;

        Debug.Log("Interact");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.TryGetComponent<Player>(out Player player)) return;

        anvilOutline.SetActive(true);
        interactUI.SetActive(true);
        readyToInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (!collision.gameObject.TryGetComponent<Player>(out Player player)) return;

        anvilOutline.SetActive(false);
        interactUI.SetActive(false);
        readyToInteract = false;
    }
}
