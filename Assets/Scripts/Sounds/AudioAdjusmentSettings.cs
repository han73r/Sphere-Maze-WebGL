using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AudioAdjusmentSettings
{
    public readonly float speedTo;
    public readonly float min;
    public readonly float max;
    public readonly float changeRate;

    public AudioAdjusmentSettings (float speedTo, float min, float max, float changeRate)
    {
        this.speedTo = speedTo;
        this.min = min;
        this.max = max;
        this.changeRate = changeRate;
    }

    public static float ClampAndInterpolate (float value, float speed, AudioAdjusmentSettings settings)
    {
        float speedBasedRollingVolume = Mathf.Clamp(speed * settings.speedTo, settings.min, settings.max);
        return Mathf.Lerp(value, speedBasedRollingVolume, settings.changeRate * Time.deltaTime);
    }
}
