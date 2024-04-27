using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugInfo : MonoBehaviour
{
    public static DebugInfo Instance;
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI speedText;
    FPSMovement f;
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
    }

    private void Start()
    {
        fpsText = GameObject.Find("FPSText").GetComponent<TextMeshProUGUI>();
        speedText = GameObject.Find("SPEEDText").GetComponent<TextMeshProUGUI>();
        f = GameObject.Find("FPSCamera").GetComponent<FPSMovement>();
        InvokeRepeating(nameof(UpdateFPS), 1, 1);
        InvokeRepeating(nameof(UpdateSpeed), 1, 1);
    }
    private void UpdateSpeed()
    {
        speedText.text = "SPEED:" + Mathf.Round(f.CurrentSpeed);
    }
    private void UpdateFPS()
    {
        int fps = (int)(1f / Time.unscaledDeltaTime);
        fpsText.text = "FPS: " + Mathf.Round(fps);
    }
}
