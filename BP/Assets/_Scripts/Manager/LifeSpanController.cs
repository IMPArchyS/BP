using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LifeSpanController : MonoBehaviour
{
    public decimal ElapsedTime { get; private set; } = 0;
    [SerializeField] private CelestialObject obj;

    private void Awake()
    {
        obj = GetComponent<CelestialObject>();
    }
    private void Start()
    {
        obj.age = 0;
    }

    private void Update()
    {
        LifeSpan();
    }

    private void LifeSpan()
    {
        ElapsedTime += (decimal)Time.smoothDeltaTime * MainTimeController.Instance.StellarTimeScale;
        ConvertToYears();
    }

    private void ConvertToYears()
    {
        decimal years = ElapsedTime / 31536000; // Number of seconds in a year
        obj.age = (long)Math.Floor(years);
    }
}
