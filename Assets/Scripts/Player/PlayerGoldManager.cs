using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoldManager : Singleton<PlayerGoldManager> {

    public Action OnGoldAdded;

    private int currentGold;


    public void AddGold(int amount) {
        currentGold += amount;
        OnGoldAdded?.Invoke();
    }

    public int GetCurrentGold() {
        return currentGold;
    }
}
