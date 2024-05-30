using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUIManager : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI text;

    private void Start() {
        PlayerGoldManager.Instance.OnGoldAdded += HandleGoldAdded;
    }

    private void HandleGoldAdded() {
        text.text = PlayerGoldManager.Instance.GetCurrentGold().ToString();
    }
}
