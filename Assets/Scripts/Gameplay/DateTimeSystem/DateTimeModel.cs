using System;
using UnityEngine;
using AlphaStrike.Gameplay.DateTimeSystem;
/// <summary>
/// !!! IMPORTANT !!!
/// Time.deltaTime uses scaled time, so use Time.unscaledDeltaTime when you need it regardless of IsPaused state!
///(ie when dealing with updating UI not dependant on game time running, you use unscaledDeltaTime instead of deltaT)
///
/// Check for IsPaused for doing Simulation to prevent stuff from happening
/// because FixedUpdate sometimes runs even if timeScale is set to 0
/// </summary>
public static class DateTimeModel
{
    public static DateTime CurrentDateTime { get; private set; } = new DateTime(1, 1, 1);

    public static bool IsPaused { get; private set; } = true;

    public static void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
    }
    private static void Resume()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }
    public static void TogglePause()
    {
        if (IsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    /// <summary>
    /// Should be run in Simulation.Update()!
    /// </summary>
    public static void Tick()
    {
        if (!IsPaused)
        {
            CurrentDateTime = CurrentDateTime.AddSeconds(Time.deltaTime);
        }
    }

    public static void SetDateTime(int[] dateTime)
    {
        Pause();
        CurrentDateTime = DateTimeUtil.DateTimeFromIntArray(dateTime);
    }
    public static int[] GetCurrentDateTime()
    {
        return DateTimeUtil.DateTimeToIntArray(CurrentDateTime);
    }

    public static void SetTimeScale(float newTimeScale)
    {
        if (!IsPaused)
        {
            Time.timeScale = newTimeScale;
        }
    }

}
