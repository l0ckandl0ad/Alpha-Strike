using UnityEngine;
using UnityEngine.UI;

public class DateTimeView : MonoBehaviour
{
    [SerializeField] private Text PlayPauseButtonText;
    [SerializeField] private Text CurrentDateTimeText;
    [SerializeField] private Text timeScaleDisplay;

    void Update()
    {
        // Display current date time in specific format
        CurrentDateTimeText.text = DateTimeModel.CurrentDateTime.ToString("G");
        timeScaleDisplay.text = "x" + Time.timeScale.ToString();

        if (Time.timeScale == 0f)
        {
            PlayPauseButtonText.text = "PLAY";
        }
        else
        {
            PlayPauseButtonText.text = "PAUSE";
        }
    }

}
