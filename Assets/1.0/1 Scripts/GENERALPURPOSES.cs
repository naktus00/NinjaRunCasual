using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddtionalRequirement
{
    public static IEnumerator WaitTimeAndDo(Action action, float time)
    {
        float t = time;
        if (time <= 0f)
            t = 0.1f;

        yield return new WaitForSeconds(t);

        if (action != null)
            action();
    }

    public static IEnumerator WaitFramesAndDo(Action action, int frameNumber)
    {
        int n = frameNumber;
        if (frameNumber <= 0)
            n = 1;

        for (int i = 0; i < n; i++)
        {
            yield return null;
        }

        if (action != null)
            action();
    }

    public static IEnumerator WaitEndofFrameAndDo(Action action)
    {
        yield return new WaitForEndOfFrame();

        if (action != null)
            action();
    }

    public static IEnumerator DoSlowmotion(float slowdownFactor, float slowdownLength)
    {
        float time = 0f;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        while (time < slowdownLength)
        {
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        yield return null;
    }
}
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

