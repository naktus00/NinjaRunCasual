using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TradeAdditional
{
    public static Action OnBought;

    public static bool Buy(int cost)
    {
        var user = DataManager.Instance.user;
        int coin = user.coin;

        if (coin < cost)
            return false;

        coin -= cost;
        user.coin = coin;

        OnBought?.Invoke();
        return true;
    }
}

public enum GAMEMODE
{
    STANDARD
}
