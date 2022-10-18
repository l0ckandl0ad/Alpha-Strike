public interface ISimulationData
{
    string GetScenarioName();
    string GetSceneName();
    /// <summary>
    /// Format: year, month, day, hour, minute, second, millisecond.
    /// </summary>
    void SetDateTime(int[] dateTime);
    /// <summary>
    /// Get DateTime in integer array format.
    /// </summary>
    /// <returns>Integer array format: year, month, day, hour, minute, second, millisecond.</returns>
    int[] GetDateTime();
    IntelData GetIntelData();
}
