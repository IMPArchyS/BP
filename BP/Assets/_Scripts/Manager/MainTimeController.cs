using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Numerics;

public class MainTimeController : MonoBehaviour
{
    #region Primary Time Properties
    public static MainTimeController Instance { get; private set; }
    public BigInteger StellarTimeScale { get; set; } = 1;
    public BigInteger YearCount { get; private set; } = 0;
    public decimal ElapsedTime { get; private set; } = 0;
    [SerializeField] private bool timePaused = false;
    #endregion

    #region Detailed Time Properties
    private BigInteger lastYearCount = 0;
    private long dayCount = 0;
    private long hourCount = 0;
    private long minuteCount = 0;
    private long secondCount = 0;
    private long currentIndex = 0;
    private float updateCounter = 0f;
    private readonly BigInteger[] timeScales = { 1, 60, 3600, 86400, 31536000, 315360000, 3153600000, 31536000000, 315360000000, 3153600000000, 31536000000000, 3153600000000000, 31536000000000000, BigInteger.Parse("31536000000000000000") };
    private readonly string[] timeUnits = { "sec", "min", "hr", "day", "yr", "10 yrs", "100 yrs", "1000 yrs", "10k yrs", "100k yrs", "1mil yrs", "100mil yrs", "1bil yrs", "1mld yrs" };
    #endregion

    #region UI
    [Header("UI time atributes")]
    [SerializeField] private TextMeshProUGUI timeScaleText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI utilText;
    #endregion

    #region Unity Events
    [SerializeField] private UnityEvent<BigInteger> onNewYear;
    [field: SerializeField] public UnityEvent<bool> OnSimToggle { get; private set; }
    #endregion

    #region DEBUG
    [Header("DEBUG SETTINGS")]
    [SerializeField] private TextMeshProUGUI debugTimeScaleText;
    #endregion

    #region Startup
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
        LinkComponents();
    }

    private void LinkComponents()
    {
        timeScaleText = GameObject.Find("MainTimeScaleText").GetComponent<TextMeshProUGUI>();
        timeText = GameObject.Find("MainTimeText").GetComponent<TextMeshProUGUI>();
        // debug only
        debugTimeScaleText = GameObject.Find("DebugTimeText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        timeText.text = "0";
        timeScaleText.text = "";
        onNewYear?.AddListener(CelestialEventManager.Instance.TriggerEvent);
    }
    #endregion

    #region Time Calculations and Text Display
    private void CalculateTime()
    {
        ElapsedTime += (decimal)Time.smoothDeltaTime * (decimal)StellarTimeScale;

        // Calculate years and remaining seconds
        decimal years = ElapsedTime / 31536000;
        YearCount = (long)Math.Floor(years);
        decimal remainingSeconds = ElapsedTime - (decimal)(YearCount * 31536000);

        // Update other time units
        dayCount = (long)(remainingSeconds / 86400) % 365;
        hourCount = (long)(remainingSeconds / 3600) % 24;
        minuteCount = (long)(remainingSeconds / 60) % 60;
        secondCount = (long)remainingSeconds % 60;
    }

    private void UpdateTimeScaleText()
    {
        if (StellarTimeScale != 0)
        {
            timeScaleText.text = TranslateToSlovak.Instance.TimeScaleToSlovak(timeUnits[currentIndex]) + " / sek";
            utilText.text = timeScaleText.text;
        }
        else
        {
            timeScaleText.text = "pozastavený";
            utilText.text = "pozastavený";
        }
    }

    private void UpdateTimeTextCounters()
    {
        updateCounter += Time.deltaTime;
        if (updateCounter >= 1f)
        {
            // debug
            string timeDisplay = $"R: {YearCount}\nD: {dayCount}\nH: {hourCount}\nM: {minuteCount}\nS: {secondCount}";
            debugTimeScaleText.text = timeDisplay;

            timeText.text = "ROK: " + YearCount.ToString("N0");
            updateCounter = 0f;
        }
    }

    private void UpdateTimeUI()
    {
        UpdateTimeScaleText();
        UpdateTimeTextCounters();
    }

    private void UpdateTime()
    {
        CalculateTime();
        UpdateTimeUI();
        if (YearCount != lastYearCount)
        {
            lastYearCount = YearCount;
            onNewYear?.Invoke(YearCount);
        }
    }
    #endregion

    #region Time Changing & Toggling
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
        OnSimToggle?.Invoke(timePaused);
    }
    #endregion

    private void Update()
    {
        UpdateTime();
    }
}
