using UnityEngine;
using TMPro;

public class DebugInfo : MonoBehaviour
{
    #region Atributes
    public static DebugInfo Instance;
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI speedText;
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
        fpsController = GameObject.Find("FPSCamera").GetComponent<FPSMovement>();
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
    #endregion
}
