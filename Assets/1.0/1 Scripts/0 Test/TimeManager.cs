using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor;
    public float slowdownLength;

    public void DoSlowmotion(float slowdownFactor, float slowdownLength)
    {
        
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

}
