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
    public int StellarYear { get; private set; } = 31536000;
    public BigInteger YearCount { get; private set; } = 0;
    public decimal ElapsedTime { get; private set; } = 0;
    public ushort Epoch { get; private set; } = 0;
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
    private readonly BigInteger[] timeScales = { 1, 60, 3600, 86400, 2628000, 31536000, 315360000, 3153600000, 31536000000, 315360000000, 3153600000000, 31536000000000, 315360000000000, 3153600000000000, 31536000000000000 };
    private readonly string[] timeUnits = { "sec", "min", "hr", "day", "month", "yr", "10 yrs", "100 yrs", "1000 yrs", "10k yrs", "100k yrs", "1mil yrs", "10mil yrs", "100mil yrs", "1bil yrs" };
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

    private void SetEpoch()
    {
        int epochIndex = PlayerPrefs.GetInt("epoch");
        switch (epochIndex)
        {
            case 2: // -3.8 bil yrs
                ElapsedTime = (decimal)25257043377494153.032970218834;
                Epoch = 2;
                break;
            case 3: // 2024
                ElapsedTime = (decimal)145344955972350710.55533790154;
                Epoch = 3;
                break;
            case 4: // +7 bil yrs
                ElapsedTime = (decimal)359540261318455788.30508691731;
                Epoch = 4;
                break;
            default:
                ElapsedTime = 0;
                Epoch = 1;
                break;
        }
    }

    private void Start()
    {
        //ElapsedTime = (decimal)31568092699542601247315.101196; // end
        SetEpoch();
        timeText.text = "";
        timeScaleText.text = "";
        onNewYear?.AddListener(CelestialEventManager.Instance.TriggerEvent);
        SoundManager.Instance.StopMusic("MAIN_MENU");
        SoundManager.Instance.PlayMusic("SIMULATION");
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

            //timeText.text = "ROK: " + YearCount.ToString("N0");
            if (YearCount >= 4600000000)
            {
                timeText.text = "+" + ((decimal)(YearCount - 4608858319) / 1000000000m).ToString("N3") + " Miliard rokov";
            }
            else
            {
                timeText.text = (((decimal)YearCount - 4600000000) / 1000000000m).ToString("N3") + " Miliard rokov";
            }
            updateCounter = 0f;
        }
    }

    private void UpdateTimeUI()
    {
        UpdateTimeScaleText();
        UpdateTimeTextCounters();
    }

    BigInteger lastYearSinceTick = 0;
    BigInteger thisYearSinceTick = 0;
    private void UpdateTime()
    {
        CalculateTime();
        lastYearSinceTick = thisYearSinceTick;
        thisYearSinceTick = YearCount;
        // calculate year change with lastYearSinceTick and thisYearSinceTick
        BigInteger diff = thisYearSinceTick - lastYearSinceTick;
        CelestialEventManager.Instance.OnStarYearly?.Invoke(diff);

        // if (diff > 0)
        // {
        //     for (BigInteger i = 0; i < diff; i++)
        //     {
        //         CelestialEventManager.Instance.OnStarYearly?.Invoke(diff);
        //     }
        // }

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
