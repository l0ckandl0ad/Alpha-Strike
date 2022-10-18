using UnityEngine;
using UnityEngine.UI;

// Refactor notes - does it need to be a singleton, actually? Merge with DateTimeView into a single VisualController?
public class DateTimeController : Singleton<DateTimeController> 
{
    [SerializeField] private Slider timeScaleSlider;
    [SerializeField] private Text timeScaleDisplay;

    private void OnEnable()
    {
        DateTimeModel.Pause();
    }

    private void OnDisable()
    {
        DateTimeModel.Pause();
    }
    public void ToggleTime()
    {
        DateTimeModel.TogglePause();
    }


    private void Update()
    {
        // Show current time scale multiplier
        //timeScaleDisplay.text = "x" + timeScaleSlider.value.ToString(); -- temp moved to DateTimeView.cs

        DateTimeModel.SetTimeScale(timeScaleSlider.value);
    }

}
