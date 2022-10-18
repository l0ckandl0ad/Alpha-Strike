using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntelWindow : MonoBehaviour
{
    [Header("Losses")]
    [SerializeField] private TMP_Text bluforLosses;
    [SerializeField] private TMP_Text opforLosses;

    [Header("Score")]
    [SerializeField] private TMP_Text bluforScore;
    [SerializeField] private TMP_Text opforScore;
    [SerializeField] private TMP_Text victor;

    [Header("Enemy Force")]
    [SerializeField] private TMP_Text enemyForce;

    private IntelData intelData;

    private void OnEnable()
    {
        intelData = GameManager.Instance.CurrentSimulation?.SimulationData?.GetIntelData();

        if (intelData != null)
        {
            intelData.OnUpdate += UpdateDisplayedData;
            UpdateDisplayedData();
        }
        else
        {
            Debug.LogError(this + ": Critical component missing!");
        }
    }

    private void OnDisable()
    {
        if (intelData != null)
        {
            intelData.OnUpdate -= UpdateDisplayedData;
        }
        else
        {
            Debug.LogError(this + ": Critical component missing!");
        }
    }

    private void UpdateDisplayedData()
    {
        bluforLosses.text = GeneratePlatformReport(intelData.BluforLosses, "<b>BLUFOR</b>:\n");
        opforLosses.text = GeneratePlatformReport(intelData.OpforLosses, "<b>OPFOR</b>:\n");

        enemyForce.text = GenerateEnemyForceReport(intelData.IntelMin, intelData.IntelMax, intelData.IntelError,
            intelData.IntelLastUpdate, "<b>ENEMY FORCE</b>:\n");

        bluforScore.text = "BLUFOR: " + intelData.BluforScore.ToString();
        opforScore.text = "OPFOR: " + intelData.OpforScore.ToString();
        DisplayVictor(victor, intelData);
    }

    private string GeneratePlatformReport(Dictionary<string, int> lostPlatforms, string reportTitle)
    {
        if (lostPlatforms == null || lostPlatforms.Count == 0) return "NO DATA";

        string report = reportTitle;

        string[] platformTypes = { "CV", "CA", "DD" }; // from enum SpacePlatformType

        foreach (string platformType in platformTypes)
        {
            if (lostPlatforms.TryGetValue(platformType, out int value))
            {
                report += "\n" + platformType + ": " + value.ToString();
            }
        }

        return report;
    }

    private string GenerateEnemyForceReport(Dictionary<string, int> intelMin,
        Dictionary<string, int> intelMax, float intelError, DateTime intelLastUpdate, string title)
    {
        string report = title;

        string[] platformTypes = { "CV", "CA", "DD" }; // from enum SpacePlatformType

        foreach (string platform in platformTypes)
        {
            if (intelMax[platform] > 0)
            {
                if (intelMin[platform] == intelMax[platform])
                {
                    report += "\n" + platform + ": " + intelMax[platform].ToString();
                }
                else
                {
                    report += "\n" + platform + ": " + intelMin[platform].ToString() + "-" +
                        intelMax[platform].ToString();
                }

            }
        }

        double lastUpdateInHours = (DateTimeModel.CurrentDateTime - intelLastUpdate).TotalHours;

        report += "\n\nReport accuracy: " + ((int)((1-intelError) * 100)).ToString() + "%";

        report += "\nLast update: " + Math.Round(lastUpdateInHours, 1).ToString() + "h ago";

        return report;
    }

    private void DisplayVictor(TMP_Text text, IntelData intelData)
    {
        switch (intelData.Victor)
        {
            case IFF.EMPTY:
                text.text = "";
                break;
            case IFF.BLUFOR:
                text.text = "BLUFOR VICTORY ACHIEVED!";
                break;
            case IFF.OPFOR:
                text.text = "OPFOR VICTORY ACHIEVED!";
                break;
            default:
                text.text = "";
                break;
        }
    }
}
