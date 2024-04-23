using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainTimeController : MonoBehaviour
{
    public static MainTimeController Instance { get; private set; }
    public long StellarTimeScale { get; private set; } = 1;
    public long YearCount { get; private set; } = 0;
    public decimal ElapsedTime { get; private set; } = 0;
    [SerializeField] private TextMeshProUGUI timeScaleText;
    [SerializeField] private TextMeshProUGUI timeText;

    private long dayCount = 0;
    private long hourCount = 0;
    private long minuteCount = 0;
    private long secondCount = 0;
    private long currentIndex = 0;
    private float updateCounter = 0f;

    private readonly long[] timeScales = { 1, 60, 3600, 86400, 31536000, 315360000, 3153600000, 31536000000, 315360000000, 3153600000000, 31536000000000 };
    private readonly string[] timeUnits = { "sec", "min", "hr", "day", "yr", "10 yrs", "100 yrs", "1000 yrs", "10k yrs", "100k yrs", "1mil yrs" };

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
        timeScaleText = GameObject.Find("TimeScaleText").GetComponent<TextMeshProUGUI>();
        timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        timeText.text = "Year: " + YearCount;
        timeScaleText.text = "Time x" + StellarTimeScale;
    }

    private void Update()
    {
        UpdateTime();
    }

    public void ChangeTimeScaler(bool forward = true)
    {
        if (forward)
            currentIndex++;
        else
            currentIndex--;

        if (currentIndex >= timeScales.Length)
            currentIndex = 0;
        else if (currentIndex < 0)
            currentIndex = timeScales.Length - 1;

        StellarTimeScale = timeScales[currentIndex];
    }

    private void CalculateTime()
    {
        ElapsedTime += (decimal)Time.smoothDeltaTime * StellarTimeScale;

        // Calculate years and remaining seconds
        decimal years = ElapsedTime / 31536000;
        YearCount = (long)Math.Floor(years);
        decimal remainingSeconds = ElapsedTime - (YearCount * 31536000);

        // Update other time units
        dayCount = (long)(remainingSeconds / 86400) % 365;
        hourCount = (long)(remainingSeconds / 3600) % 24;
        minuteCount = (long)(remainingSeconds / 60) % 60;
        secondCount = (long)remainingSeconds % 60;
    }

    private void UpdateTimeUI()
    {
        timeScaleText.text = "TIME: " + timeUnits[currentIndex] + " / sec";
        updateCounter += Time.deltaTime;
        if (updateCounter >= 1f)
        {
            string timeDisplay = timeUnits[currentIndex] switch
            {
                "sec" => $"Y: {YearCount}, D: {dayCount}, H: {hourCount}, M: {minuteCount}, S: {secondCount}",
                "min" => $"Y: {YearCount}, D: {dayCount}, H: {hourCount}, M: {minuteCount}",
                "hr" => $"Y: {YearCount}, D: {dayCount}, H: {hourCount}",
                "day" => $"Y: {YearCount}, D: {dayCount}",
                "yr" => $"Y: {YearCount}",
                _ => $"Y: {YearCount}",
            };
            timeText.text = timeDisplay;
            updateCounter = 0f;
        }
    }

    private void UpdateTime()
    {
        CalculateTime();
        UpdateTimeUI();
        UpdateTimeUI();
    }
}
