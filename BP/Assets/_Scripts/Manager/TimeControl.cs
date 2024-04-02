using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeControl : MonoBehaviour
{
    public static TimeControl Instance { get; private set; }
    public long StellarTimeScale = 1;
    private readonly long[] timeScales = { 1, 60, 3600, 86400, 31536000, 315360000, 3153600000, 31536000000, 315360000000, 3153600000000, 31536000000000 };
    private readonly string[] timeUnits = { "sec", "min", "hr", "day", "yr", "10 yrs", "100 yrs", "1000 yrs", "10k yrs", "100k yrs", "1mil yrs" };
    [SerializeField] private Button timeControlButton;
    [SerializeField] private TextMeshProUGUI timeScaleText;
    [SerializeField] private TextMeshProUGUI timeText;
    private decimal elapsedTime = 0;
    private long yearCount = 0;
    private long dayCount = 0;
    private long hourCount = 0;
    private long minuteCount = 0;
    private long secondCount = 0;
    private long currentIndex = 0;
    private float updateCounter = 0f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        timeControlButton = GameObject.FindGameObjectWithTag("TimeController").GetComponent<Button>();
        timeScaleText = GameObject.Find("TimeScaleText").GetComponent<TextMeshProUGUI>();
        timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        timeText.text = "Year: " + yearCount;
        timeScaleText.text = "Time x" + StellarTimeScale;
    }

    private void Update()
    {
        UpdateTime();
    }


    public void UpdateTime()
    {
        elapsedTime += (decimal)Time.smoothDeltaTime * StellarTimeScale;

        // Calculate years and remaining seconds
        decimal years = elapsedTime / 31536000; // Number of seconds in a year
        yearCount = (long)Math.Floor(years);
        decimal remainingSeconds = elapsedTime - (yearCount * 31536000);

        // Update other time units
        dayCount = (long)(remainingSeconds / 86400) % 365;
        hourCount = (long)(remainingSeconds / 3600) % 24;
        minuteCount = (long)(remainingSeconds / 60) % 60;
        secondCount = (long)remainingSeconds % 60;

        timeScaleText.text = "TIME: " + timeUnits[currentIndex] + " / sec";
        updateCounter += Time.deltaTime;
        if (updateCounter >= 1f) // If one second has passed
        {
            string timeDisplay = $"Y: {yearCount}, D: {dayCount}, H: {hourCount}, M: {minuteCount}, S: {secondCount}";

            if (timeUnits[currentIndex] == "min")
            {
                timeDisplay = $"Y: {yearCount}, D: {dayCount}, H: {hourCount}, M: {minuteCount}";
            }
            else if (timeUnits[currentIndex] == "hr")
            {
                timeDisplay = $"Y: {yearCount}, D: {dayCount}, H: {hourCount}";
            }
            else if (timeUnits[currentIndex] == "day")
            {
                timeDisplay = $"Y: {yearCount}, D: {dayCount}";
            }
            else if (timeUnits[currentIndex] == "yr" || currentIndex > 4)
            {
                timeDisplay = $"Y: {yearCount}";
            }

            timeText.text = timeDisplay;
            updateCounter = 0f;
        }
    }

    public void ChangeTimeScaler()
    {
        currentIndex++;
        if (currentIndex >= timeScales.Length)
        {
            currentIndex = 0;
        }
        StellarTimeScale = timeScales[currentIndex];
    }

}
