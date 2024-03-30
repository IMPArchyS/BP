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
    public int StellarTimeScale = 1;
    [SerializeField] private Button timeControlButton;
    [SerializeField] private TextMeshProUGUI timeScaleText;
    [SerializeField] private TextMeshProUGUI timeText;
    private float elapsedTime = 0f;
    private BigInteger yearCount = 0;
    private int dayCount = 0;
    private int hourCount = 0;
    private int minuteCount = 0;
    private int secondCount = 0;

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
        timeText.text = $"Year: {yearCount}, Day: {dayCount}, Hour: {hourCount}, Minute: {minuteCount}, Second: {secondCount}";
    }


    public void UpdateTime()
    {
        elapsedTime += Time.deltaTime * StellarTimeScale;

        secondCount = (int)elapsedTime % 60;
        minuteCount = (int)(elapsedTime / 60) % 60;
        hourCount = (int)(elapsedTime / 3600) % 24;
        dayCount = (int)(elapsedTime / 86400) % 365;
        yearCount = (int)(elapsedTime / 31536000); // Assuming a year is 365 days

        // Reset elapsed time every "stellar" year to prevent it from becoming too large
        if (yearCount > 0)
        {
            elapsedTime = 0;
        }

        timeScaleText.text = "Time x" + StellarTimeScale;
    }

}
