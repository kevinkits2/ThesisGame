using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour {

    [SerializeField] private GameObject playerSkillTreeContainer;
    private bool skillTreeContainerOpened;


    private void Awake() {
        playerSkillTreeContainer.SetActive(false);
    }

    private void Start() {
        InputManager.Instance.OnSkillTreeAction += HandleSkillTreeAction;
    }

    private void HandleSkillTreeAction(object sender, System.EventArgs e) {
        if (skillTreeContainerOpened) {
            CloseSkillTreeContainer();
        }
        else {
            OpenSkillTreeContainer();
        }
    }

    private void OpenSkillTreeContainer() {
        InputManager.Instance.DisablePlayerControls();
        playerSkillTreeContainer.SetActive(true);

        skillTreeContainerOpened = true;
    }

    private void CloseSkillTreeContainer() {
        InputManager.Instance.EnablePlayerControls();
        playerSkillTreeContainer.SetActive(false);

        skillTreeContainerOpened = false;
    }
}
