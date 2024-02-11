using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeControl : MonoBehaviour
{
    [SerializeField] private Button timeControlButton;
    [SerializeField] private TextMeshProUGUI timeScaleText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private float stelarTimeScale;
    private float elapsedTime = 0f;
    private int yearCount = 0;

    private void Awake()
    {
        timeControlButton = GameObject.FindGameObjectWithTag("TimeController").GetComponent<Button>();
        timeScaleText = GameObject.Find("TimeScaleText").GetComponent<TextMeshProUGUI>();
        timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        timeText.text = "Year: " + yearCount;
        timeScaleText.text = "Time x" + Time.timeScale;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= stelarTimeScale)
        {
            elapsedTime -= stelarTimeScale;
            yearCount++;
            timeText.text = "Year: " + yearCount;
        }
    }

    public void ChangeTimeScale() 
    {
        if (Time.timeScale == 64)
            Time.timeScale = 100;
        else if(Time.timeScale < 64)
            Time.timeScale *= 2;
        else 
            Time.timeScale = 1;

        timeScaleText.text = "Time x" + Time.timeScale;
    }

}
