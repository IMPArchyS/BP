using UnityEngine;
using TMPro;
using System.Linq;

public class DebugInfo : MonoBehaviour
{
    #region Atributes
    public static DebugInfo Instance;
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI debugText;
    public TextMeshProUGUI memText;
    public TextMeshProUGUI renderText;
    public TextMeshProUGUI activeObjectText;
    public GameObject debugCanvas;
    public int maxDebugLines;
    private FPSMovement fpsController;
    #endregion

    #region Startup
    private void Awake()
    {
        CreateSingletonInstance();
    }

    private void Start()
    {
        LinkGUIObjects();
        StartTextUpdates();
    }

    private void CreateSingletonInstance()
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
    }

    private void StartTextUpdates()
    {
        InvokeRepeating(nameof(UpdateFPS), 1, 1);
        InvokeRepeating(nameof(UpdateSpeed), 1, 1);
    }

    private void LinkGUIObjects()
    {
        fpsText = GameObject.Find("FPSText").GetComponent<TextMeshProUGUI>();
        speedText = GameObject.Find("SPEEDText").GetComponent<TextMeshProUGUI>();
        debugText = GameObject.Find("DebugLogText").GetComponent<TextMeshProUGUI>();
        fpsController = GameObject.Find("FPSCamera").GetComponent<FPSMovement>();
        debugCanvas = GameObject.Find("DEBUGCanvas");
    }
    #endregion

    #region UI update
    private void UpdateSpeed()
    {
        speedText.text = "SPEED:" + Mathf.Round(fpsController.CurrentSpeed);
    }

    private void UpdateFPS()
    {
        int fps = (int)(1f / Time.unscaledDeltaTime);
        fpsText.text = "FPS: " + Mathf.Round(fps);
    }

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        string color = "white";
        switch (type)
        {
            case LogType.Warning:
                color = "yellow";
                break;
            case LogType.Error:
            case LogType.Exception:
                color = "red";
                break;
        }

        string newLog = $"<color={color}>{logString}\n{stackTrace}</color>\n";
        string[] lines = (newLog + debugText.text).Split('\n');

        // Limit to a certain number of lines
        if (lines.Length >= maxDebugLines)
        {
            lines = lines.Take(maxDebugLines).ToArray();
        }

        debugText.text = string.Join("\n", lines);
    }
    private void ShowMemoryUsage()
    {
        long totalMemory = System.GC.GetTotalMemory(false);
        memText.text = "";
        memText.text = $"Memory: {totalMemory / (1024 * 1024)} MB\n" + memText.text;
    }

    private void ShowAmountOfRenderedObjects()
    {
        int renderedObjects = QualitySettings.pixelLightCount;
        renderText.text = "";
        renderText.text = $"Rendered Objects: {renderedObjects}";
    }

    private void ShowAmountOfActiveGameObjects()
    {
        int activeGameObjects = FindObjectsOfType<GameObject>().Count(go => go.activeInHierarchy);
        activeObjectText.text = "";
        activeObjectText.text = $"Active GameObjects: {activeGameObjects}";
    }

    private void ToggleUI()
    {
        if (Input.GetKeyDown(KeyCode.M))
            debugCanvas.SetActive(!debugCanvas.activeInHierarchy);
    }
    #endregion


    private void Update()
    {
        ShowMemoryUsage();
        ShowAmountOfRenderedObjects();
        ShowAmountOfActiveGameObjects();
        ToggleUI();
    }
}
