using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartEmpty : LevelPart
{
    public Transform axeSlotsParent;

    private float _r = 0f;
    public float r
    {
        get
        {
            if (sample == 0)
                _r = 2.25f;

            else if(sample == 1)
                _r = 6f;

            return _r;
        }
    }

    public static float GetR(int sample)
    {
        float r = 0f;

        if (sample == 0)
            r = 2.25f;

        else if (sample == 1)
            r = 6f;

        return r;

    }

}
