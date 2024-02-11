using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugInfo : MonoBehaviour
{
    public static DebugInfo instance;
    public TextMeshProUGUI fpsText;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        fpsText = GameObject.Find("FPSText").GetComponent<TextMeshProUGUI>();
        StartCoroutine(UpdateFPS());
    }

    private IEnumerator UpdateFPS()
    {
        while (true)
        {
            float fps = 1f / Time.deltaTime;
            fpsText.text = "FPS: " + Mathf.Round(fps);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
