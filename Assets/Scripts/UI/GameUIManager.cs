using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour {

    public static GameUIManager Instance;

    public event Action<PlayerSkillSO> OnSkillButtonClicked;

    [SerializeField] private GameObject playerSkillTreeContainer;
    [SerializeField] private GameObject playerStatsContainer;

    private bool skillTreeContainerOpened;
    private bool statsContainerOpened;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("GameUIManager instance already exists!!");

            return;
        }

        Instance = this;
    }

    private void Start() {
        InputManager.Instance.OnSkillTreeAction += HandleSkillTreeAction;

        SkillTreeUI skillTreeUI = playerSkillTreeContainer.GetComponent<SkillTreeUI>();
        skillTreeUI.OnSkillButtonClicked += HandleSkillButtonClicked;

        InputManager.Instance.OnStatsAction += HandleStatsAction;

        playerSkillTreeContainer.SetActive(false);
        playerStatsContainer.SetActive(false);
    }

    private void HandleStatsAction(object sender, EventArgs e) {
        if (statsContainerOpened) {
            CloseStatsContainer();
        }
        else {
            OpenStatsContainer();
        }
    }

    private void HandleSkillButtonClicked(PlayerSkillSO obj) {
        OnSkillButtonClicked?.Invoke(obj);
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

    private void OpenStatsContainer() {
        InputManager.Instance.DisablePlayerControls();
        playerStatsContainer.SetActive(true);

        statsContainerOpened = true;
    }

    private void CloseStatsContainer() {
        InputManager.Instance.EnablePlayerControls();
        playerStatsContainer.SetActive(false);

        statsContainerOpened = false;
    }
}
