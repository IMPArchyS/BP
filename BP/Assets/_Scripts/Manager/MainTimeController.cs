using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainTimeController : MonoBehaviour
{
    #region Primary time atributes
    public static MainTimeController Instance { get; private set; }
    [Header("Primary time atributes")]
    [SerializeField] private TextMeshProUGUI timeScaleText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI utilText;
    [field: SerializeField] public long StellarTimeScale { get; set; } = 1;
    [field: SerializeField] public long YearCount { get; private set; } = 0;
    [SerializeField] private bool timePaused = false;
    public decimal ElapsedTime { get; private set; } = 0;

    #endregion

    #region detailAtributes

    private long dayCount = 0;
    private long hourCount = 0;
    private long minuteCount = 0;
    private long secondCount = 0;
    private long currentIndex = 0;
    private float updateCounter = 0f;
    private readonly long[] timeScales = { 1, 60, 3600, 86400, 31536000, 315360000, 3153600000, 31536000000, 315360000000, 3153600000000, 31536000000000 };
    private readonly string[] timeUnits = { "sec", "min", "hr", "day", "yr", "10 yrs", "100 yrs", "1000 yrs", "10k yrs", "100k yrs", "1mil yrs" };

    #endregion

    #region DEBUG

    [Header("DEBUG SETTINGS")]
    [SerializeField] private TextMeshProUGUI debugTimeScaleText;
    #endregion
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        timeScaleText = GameObject.Find("MainTimeScaleText").GetComponent<TextMeshProUGUI>();
        timeText = GameObject.Find("MainTimeText").GetComponent<TextMeshProUGUI>();
        // debug only
        debugTimeScaleText = GameObject.Find("DebugTimeText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        timeText.text = "0" + YearCount;
        timeScaleText.text = "";
    }

    private void Update()
    {
        UpdateTime();
    }

    public void ChangeTimeScaler(bool forward = true)
    {
        if (StellarTimeScale == 0) return;

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
        if (StellarTimeScale != 0)
        {
            timeScaleText.text = TimeScaleToSlovak(timeUnits[currentIndex]) + " / sek";
            utilText.text = timeScaleText.text;
        }
        else
        {
            timeScaleText.text = "pozastavený";
            utilText.text = "pozastavený";
        }

        updateCounter += Time.deltaTime;
        if (updateCounter >= 1f)
        {
            // debug
            string timeDisplay = $"R: {YearCount}\nD: {dayCount}\nH: {hourCount}\nM: {minuteCount}\nS: {secondCount}";
            debugTimeScaleText.text = timeDisplay;

            timeText.text = "ROK: " + YearCount.ToString();
            updateCounter = 0f;
        }
    }

    private void UpdateTime()
    {
        CalculateTime();
        UpdateTimeUI();
        UpdateTimeUI();
    }

    private string TimeScaleToSlovak(string timeUnit)
    {
        string skUnit = "";
        switch (timeUnit)
        {
            case "sec":
                skUnit = "sek";
                break;
            case "min":
                skUnit = "min";
                break;
            case "hr":
                skUnit = "hod";
                break;
            case "day":
                skUnit = "deň";
                break;
            case "yr":
                skUnit = "rok";
                break;
            case "10 yrs":
                skUnit = "10 rokov";
                break;
            case "100 yrs":
                skUnit = "100 rokov";
                break;
            case "1000 yrs":
                skUnit = "1000 rokov";
                break;
            case "10k yrs":
                skUnit = "10k rokov";
                break;
            case "100k yrs":
                skUnit = "100k rokov";
                break;
            case "1mil yrs":
                skUnit = "1mil rokov";
                break;
        }
        return skUnit;
    }

    public void ResetTimeScale()
    {
        currentIndex = 0;
        StellarTimeScale = 1;
    }

    public void ToggleTime()
    {
        if (!timePaused)
        {
            StellarTimeScale = 0;
            timePaused = true;
        }
        else
        {
            timePaused = false;
            StellarTimeScale = timeScales[currentIndex];
        }
    }
}
