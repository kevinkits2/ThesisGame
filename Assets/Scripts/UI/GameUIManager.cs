using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour {

    public static GameUIManager Instance;

    public event Action<PlayerSkillSO> OnSkillButtonClicked;

    [SerializeField] private GameObject playerSkillTreeContainer;
    private bool skillTreeContainerOpened;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("GameUIManager instance already exists!!");

            return;
        }

        Instance = this;

        playerSkillTreeContainer.SetActive(false);
    }

    private void Start() {
        InputManager.Instance.OnSkillTreeAction += HandleSkillTreeAction;

        SkillTreeUI skillTreeUI = playerSkillTreeContainer.GetComponent<SkillTreeUI>();
        skillTreeUI.OnSkillButtonClicked += HandleSkillButtonClicked;
    }

    private void HandleSkillButtonClicked(PlayerSkillSO obj) {
        OnSkillButtonClicked.Invoke(obj);
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
