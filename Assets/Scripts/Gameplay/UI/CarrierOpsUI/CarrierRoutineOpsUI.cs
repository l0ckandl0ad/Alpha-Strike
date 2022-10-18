using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrierRoutineOpsUI : MonoBehaviour
{
    private ICarrierOpsHandler carrierCommandHandler;
    private IRoutineCarrierOpsSettings currentRoutineOpsSettings;
    private GameObject canvasGameObject;

    [Header("COMPAS ROSE")]
    [SerializeField] private Toggle mainToggle;

    [SerializeField] private Toggle zeroDegrees;
    [SerializeField] private Toggle fortyFiveDegrees;
    [SerializeField] private Toggle ninetyDegrees;
    [SerializeField] private Toggle oneThirtyFiveDegrees;

    [SerializeField] private Toggle oneEightyDegrees;
    [SerializeField] private Toggle twoTwentyFiveDegrees;
    [SerializeField] private Toggle twoSeventyDegrees;
    [SerializeField] private Toggle threeFifteenDegrees;

    [Header("RANGE")]
    [SerializeField] private Slider rangeSlider;
    [SerializeField] private Text rangeText;

    [Header("FREQUENCY")]
    [SerializeField] private Slider frequencySlider;
    [SerializeField] private Text frequencyText;

    [Header("OTHER OPTIONS")]
    [SerializeField] private Toggle shadow;
    [SerializeField] private Button searchNow;

    private List<Toggle> compasRoseToggleList;

    private void Awake()
    {
        Canvas canvas = GetComponentInChildren<Canvas>();
        canvasGameObject = canvas.gameObject;
        Hide();

        compasRoseToggleList = new List<Toggle>();
        compasRoseToggleList.Add(zeroDegrees);
        compasRoseToggleList.Add(fortyFiveDegrees);
        compasRoseToggleList.Add(ninetyDegrees);
        compasRoseToggleList.Add(oneThirtyFiveDegrees);

        compasRoseToggleList.Add(oneEightyDegrees);
        compasRoseToggleList.Add(twoTwentyFiveDegrees);
        compasRoseToggleList.Add(twoSeventyDegrees);
        compasRoseToggleList.Add(threeFifteenDegrees);

    }
    private void SubscribeToValueChanges()
    {
        mainToggle.onValueChanged.AddListener((ctx) => MainToggle());

        zeroDegrees.onValueChanged.AddListener((ctx) => SetCurrentRoutineOpsSettings());
        fortyFiveDegrees.onValueChanged.AddListener((ctx) => SetCurrentRoutineOpsSettings());
        ninetyDegrees.onValueChanged.AddListener((ctx) => SetCurrentRoutineOpsSettings());
        oneThirtyFiveDegrees.onValueChanged.AddListener((ctx) => SetCurrentRoutineOpsSettings());

        oneEightyDegrees.onValueChanged.AddListener((ctx) => SetCurrentRoutineOpsSettings());
        twoTwentyFiveDegrees.onValueChanged.AddListener((ctx) => SetCurrentRoutineOpsSettings());
        twoSeventyDegrees.onValueChanged.AddListener((ctx) => SetCurrentRoutineOpsSettings());
        threeFifteenDegrees.onValueChanged.AddListener((ctx) => SetCurrentRoutineOpsSettings());

        rangeSlider.onValueChanged.AddListener((ctx) => SetCurrentRoutineOpsSettings());
        frequencySlider.onValueChanged.AddListener((ctx) => SetCurrentRoutineOpsSettings());

        shadow.onValueChanged.AddListener((ctx) => SetCurrentRoutineOpsSettings());
        searchNow.onClick.AddListener(() => SearchNow());
    }
    private void UnsubscribeToValueChanges()
    {
        mainToggle.onValueChanged.RemoveAllListeners();

        zeroDegrees.onValueChanged.RemoveAllListeners();
        fortyFiveDegrees.onValueChanged.RemoveAllListeners();
        ninetyDegrees.onValueChanged.RemoveAllListeners();
        oneThirtyFiveDegrees.onValueChanged.RemoveAllListeners();

        oneEightyDegrees.onValueChanged.RemoveAllListeners();
        twoTwentyFiveDegrees.onValueChanged.RemoveAllListeners();
        twoSeventyDegrees.onValueChanged.RemoveAllListeners();
        threeFifteenDegrees.onValueChanged.RemoveAllListeners();

        rangeSlider.onValueChanged.RemoveAllListeners();
        frequencySlider.onValueChanged.RemoveAllListeners();

        shadow.onValueChanged.RemoveAllListeners();
        searchNow.onClick.RemoveAllListeners();
    }
    private void OnDisable()
    {
        UnsubscribeToValueChanges();
    }
    public void Show(ICarrierOpsHandler carrierCommandHandler)
    {
        canvasGameObject.SetActive(true);
        UnsubscribeToValueChanges();
        this.carrierCommandHandler = carrierCommandHandler;
        currentRoutineOpsSettings = carrierCommandHandler.RoutineOpsSettings;
        ReadSearchSettings(currentRoutineOpsSettings);
        mainToggle.isOn = false;
        SubscribeToValueChanges();
    }
    public void Hide()
    {
        UnsubscribeToValueChanges();
        canvasGameObject.SetActive(false);
    }
    
    private void ReadSearchSettings(IRoutineCarrierOpsSettings routineOpsSettings)
    {
        rangeSlider.value = routineOpsSettings.Range;
        rangeText.text = routineOpsSettings.Range.ToString();
        frequencySlider.value = routineOpsSettings.SearchFrequencyInMinutes;
        frequencyText.text = routineOpsSettings.SearchFrequencyInMinutes.ToString();

        zeroDegrees.isOn = routineOpsSettings.ZeroDegrees;
        fortyFiveDegrees.isOn = routineOpsSettings.FortyFiveDegrees;
        ninetyDegrees.isOn = routineOpsSettings.NinetyDegrees;
        oneThirtyFiveDegrees.isOn = routineOpsSettings.OneThirtyFiveDegrees;

        oneEightyDegrees.isOn = routineOpsSettings.OneEightyDegrees;
        twoTwentyFiveDegrees.isOn = routineOpsSettings.TwoTwentyFiveDegrees;
        twoSeventyDegrees.isOn = routineOpsSettings.TwoSeventyDegrees;
        threeFifteenDegrees.isOn = routineOpsSettings.ThreeFifteenDegrees;

        shadow.isOn = routineOpsSettings.Shadow;
    }
    private void SetRoutineOpsSettings(IRoutineCarrierOpsSettings routineOpsSettings)
    {
        routineOpsSettings.SetRange(rangeSlider.value);
        routineOpsSettings.SetSearchFrequencyInMinutes(frequencySlider.value);

        routineOpsSettings.SetZeroDegrees(zeroDegrees.isOn);
        routineOpsSettings.SetFortyFiveDegrees(fortyFiveDegrees.isOn);
        routineOpsSettings.SetNinetyDegrees(ninetyDegrees.isOn);
        routineOpsSettings.SetOneThirtyFiveDegrees(oneThirtyFiveDegrees.isOn);

        routineOpsSettings.SetOneEightyDegrees(oneEightyDegrees.isOn);
        routineOpsSettings.SetTwoTwentyFiveDegrees(twoTwentyFiveDegrees.isOn);
        routineOpsSettings.SetTwoSeventyDegrees(twoSeventyDegrees.isOn);
        routineOpsSettings.SetThreeFifteenDegrees(threeFifteenDegrees.isOn);

        routineOpsSettings.SetShadow(shadow.isOn);
    }
    private void SetCurrentRoutineOpsSettings()
    {
        SetRoutineOpsSettings(currentRoutineOpsSettings);
        rangeText.text = rangeSlider.value.ToString();
        frequencyText.text = frequencySlider.value.ToString();
    }
    private void MainToggle()
    {
        if (mainToggle.isOn)
        {
            foreach (Toggle toggle in compasRoseToggleList)
            {
                toggle.isOn = false;
            }
        }
        else
        {
            foreach (Toggle toggle in compasRoseToggleList)
            {
                toggle.isOn = true;
            }
        }
    }
    private void SearchNow()
    {
        carrierCommandHandler.RunRoutineSearchNow();
    }
}
