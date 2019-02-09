using UnityEngine;
using System.Collections;

public class TimeHelper
{
    public static float time
    { get { return Time.time; } }

    public static float realtimeSinceStartup
    { get { return Time.realtimeSinceStartup; } }

    public static float updateDeltaTime
    { get { return Time.deltaTime; } }

    public static float timeScale
    { get { return Time.timeScale; } }

    public static float DurationTime(float _time)
    {
        return time - _time;
    }

    public static void SlowTime()
    {
        Time.timeScale = 0.1f;
    }

    public static void NormalTime()
    {
        Time.timeScale = 1.0f;
    }

    public static void Pause()
    {
        Debug.Log("Time Pause");

        m_fPauseTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;
    }

    public static void Resume()
    {
        Debug.Log("Time Resume");

        if (Time.timeScale != 0.0f)
            return;

        Time.timeScale = m_fPauseTimeScale;
    }

    public static void Reset()
    {
        Debug.Log("Time Reset");

        Time.timeScale = 1.0f;
    }

    private static float m_fPauseTimeScale;
}